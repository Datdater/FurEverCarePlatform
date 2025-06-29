using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Create
{
    public class CreateOrderHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateOrderCommand>
    {
        public async Task Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            float totalPrice = 0;
            foreach (var item in request.OrderDetails)
            {
                var productVariation = await unitOfWork
                    .GetRepository<ProductVariant>()
                    .GetQueryable()
                    .Include(x => x.Product)
                    .FirstOrDefaultAsync(x => x.Id == item.ProductVariationId, cancellationToken);
                totalPrice += productVariation.Price * item.Quantity;
            }

            var order = new Order
            {
                AddressId = request.AddressId,
                UserId = request.CustomerId,
                TotalPrice = totalPrice,
                Code = GenerateOrderCode(),
                OrderDetails = request
                    .OrderDetails.Select(x => new OrderDetail
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
                OrderStatus = Domain.Enums.EnumOrderStatus.Pending,
                Payment = new Payment { PaymentMethod = request.PaymentMethod },
            };
            await unitOfWork.GetRepository<Order>().InsertAsync(order);
            await unitOfWork.SaveAsync();
        }

        private string GenerateOrderCode()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
}
