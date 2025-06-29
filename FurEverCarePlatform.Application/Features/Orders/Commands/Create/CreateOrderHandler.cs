using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Order.Application.Feature.Orders.Commands
{
    public class CreateOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            decimal totalPrice = 0;
            foreach (var item in request.OrderDetails)
            {
                var productVariation = await unitOfWork
                    .GetRepository<ProductVariant>()
                    .GetQueryable()
                    .Include(x => x.Product)
                    .FirstOrDefaultAsync(x => x.Id == item.ProductVariationId, cancellationToken);
                totalPrice += productVariation.Price * item.Quantity;
            }

            var order = new FurEverCarePlatform.Domain.Entities.Order
            {
                AddressId = request.AddressId,
                UserId = request.CustomerId,
                TotalPrice = totalPrice,
                Code = GenerateOrderCode(),
                OrderDetails = request
                    .OrderDetails.Select(x => new FurEverCarePlatform.Domain.Entities.OrderDetail
                    {
                        ProductVariationId = x.ProductVariationId,
                        Quantity = x.Quantity,
                        Price =
                            x.Quantity
                            * unitOfWork
                                .GetRepository<ProductVariant>()
                                .GetQueryable()
                                .Where(pv => pv.Id == x.ProductVariationId)
                                .Select(pv => pv.Price)
                                .FirstOrDefault(),
                    })
                    .ToList(),
                OrderStatus = FurEverCarePlatform.Domain.Enums.EnumOrderStatus.Pending,
                Payment = new FurEverCarePlatform.Domain.Entities.Payment
                {
                    PaymentMethod = request.PaymentMethod,
                },
            };
            await unitOfWork
                .GetRepository<FurEverCarePlatform.Domain.Entities.Order>()
                .InsertAsync(order);
            await unitOfWork.SaveAsync();
        }

        private string GenerateOrderCode()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
}
