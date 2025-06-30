using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllOrdersQuery, Pagination<GetAllOrdersResponse>>
    {
        public async Task<Pagination<GetAllOrdersResponse>> Handle(
            GetAllOrdersQuery request,
            CancellationToken cancellationToken
        )
        {
            var orders = unitOfWork
                .GetRepository<Domain.Entities.Order>()
                .GetQueryable()
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.ProductVariation)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.Store)
                .Include(o => o.AppUser)
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.ProductVariation)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.Images)
                .Where(o => !o.IsDeleted);

            var orderPagination = await Pagination<Domain.Entities.Order>.CreateAsync(
                orders,
                request.PageNumber,
                request.PageSize
            );
            var orderResponseQueryable = orderPagination
                .Items.Select(o => new GetAllOrdersResponse()
                {
                    Id = o.Id,
                    CreatedTime = o.OrderDate,
                    StoreName =
                        o.OrderDetails.FirstOrDefault()?.ProductVariation?.Product?.Store?.Name
                        ?? "Unknown Store",
                    StoreId =
                        o.OrderDetails.FirstOrDefault()?.ProductVariation?.Product?.Store?.Id
                        ?? Guid.Empty,
                    Price = (decimal)o.OrderDetails.Sum(od => od.Price * od.Quantity),
                    OrderDetailDTOs = o
                        .OrderDetails.Select(od => new GetOrderDetail()
                        {
                            ProductVariationId = od.ProductVariationId,
                            Quantity = od.Quantity,
                            Price = (decimal)od.Price,
                            Attribute = od.ProductVariation?.Attributes,
                            PictureUrl =
                                od.ProductVariation?.Product?.Images.FirstOrDefault(i =>
                                    i.IsMain
                                )?.ImageUrl ?? string.Empty,
                            ProductId = od.ProductVariation?.Product?.Id ?? Guid.Empty,
                            ProductName = od.ProductVariation?.Product?.Name ?? "Unknown Product",
                        })
                        .ToList(),
                    DeliveryPrice = o.DeliveryPrice,
                    OrderStatus = o.OrderStatus.ToString(),
                })
                .ToList();
            var response = new Pagination<GetAllOrdersResponse>(
                orderResponseQueryable,
                request.PageNumber,
                request.PageSize,
                orderPagination.TotalCount
            );

            return response;
        }
    }
}
