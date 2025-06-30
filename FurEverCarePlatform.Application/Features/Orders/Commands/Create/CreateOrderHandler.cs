using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Create
{
    public class CreateOrderHandler(IUnitOfWork unitOfWork, UserManager<AppUser> userManager)
        : IRequestHandler<CreateOrderCommand>
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
            var user = await userManager.FindByIdAsync(request.CustomerId.ToString());
            if (user == null)
            {
                throw new System.Exception("User not found");
            }
            var orderDetails = new List<OrderDetail>();
            foreach (var item in request.OrderDetails)
            {
                var productVariation = await unitOfWork
                    .GetRepository<ProductVariant>()
                    .GetQueryable()
                    .Include(x => x.Product)
                    .FirstOrDefaultAsync(x => x.Id == item.ProductVariationId, cancellationToken);
                if (productVariation == null)
                {
                    throw new System.Exception(
                        $"Product variation with ID {item.ProductVariationId} not found."
                    );
                }
                if (productVariation.Stock < item.Quantity)
                {
                    throw new System.Exception(
                        $"Insufficient stock for product variation {item.ProductVariationId}. Available: {productVariation.Stock}, Requested: {item.Quantity}."
                    );
                }
                orderDetails.Add(
                    new OrderDetail
                    {
                        ProductVariationId = item.ProductVariationId,
                        Quantity = item.Quantity,
                        Price = productVariation.Price * item.Quantity,
                        ProductVariationName = productVariation.Attributes.ToString(),
                        ProductionName = productVariation.Product.Name,
                    }
                );
            }
            var order = new Order
            {
                AddressId = request.AddressId,
                Note = request.Note,
                AppUserId = request.CustomerId,
                AppUser = user,
                TotalPrice = totalPrice,
                Code = GenerateOrderCode(),
                OrderDetails = orderDetails,
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
