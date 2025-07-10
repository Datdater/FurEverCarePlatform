using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Net.payOS;
using Net.payOS.Types;
using Payment.API.DTO;

namespace FurEverCarePlatform.Application.Features.Orders.Commands.Create
{
    public class CreateOrderHandler(
        IUnitOfWork unitOfWork,
        PayOS payOS,
        IConfiguration configuration,
        UserManager<AppUser> userManager
    ) : IRequestHandler<CreateOrderCommand, PaymentCreatedResponse>
    {
        public async Task<PaymentCreatedResponse> Handle(
            CreateOrderCommand request,
            CancellationToken cancellationToken
        )
        {
            await unitOfWork.BeginTransactionAsync();
            try
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
                    DeliveryPrice = request.DeliveryPrice,
                    TotalPrice = totalPrice,
                    Code = GenerateOrderCode(),
                    OrderDetails = orderDetails,
                    OrderStatus = Domain.Enums.EnumOrderStatus.Confirmed,
                    Payment = new Domain.Entities.Payment { PaymentMethod = request.PaymentMethod },
                };
                await unitOfWork.GetRepository<Order>().InsertAsync(order);
                await unitOfWork.SaveAsync();
                if (request.PaymentMethod == Domain.Enums.PaymentMethod.BankTransfer)
                {
                    long orderCode = long.Parse(DateTimeOffset.Now.ToString("mmssffffff"));
                    List<ItemData> items = new List<ItemData>();
                    long expried = DateTimeOffset.Now.ToUnixTimeSeconds() + 15 * 60;
                    PaymentData paymentData = new PaymentData(
                        orderCode,
                        (int)totalPrice,
                        $"Senandpet",
                        items,
                        configuration["PayOS:CancelUrl"],
                        configuration["PayOS:ReturnUrl"],
                        null,
                        null,
                        null,
                        null,
                        null,
                        expried
                    );
                    CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
                    var paymentUrl = createPayment.checkoutUrl;
                    var paymentCode = createPayment.orderCode.ToString();
                    order.Payment.Code = paymentCode;
                    order.Payment.Amount = order.TotalPrice;
                    order.OrderStatus = Domain.Enums.EnumOrderStatus.PendingPayment;
                    unitOfWork.GetRepository<Order>().Update(order);
                    unitOfWork.GetRepository<Domain.Entities.Payment>().Update(order.Payment);
                    await unitOfWork.SaveAsync();
                    await unitOfWork.CommitTransactionAsync();
                    return new PaymentCreatedResponse
                    {
                        Id = order.Payment.Id,
                        PaymentUrl = paymentUrl,
                    };
                }
                else
                {
                    await unitOfWork.CommitTransactionAsync();
                    return null;
                }
            }
            catch 
            {
                await unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }

        private string GenerateOrderCode()
        {
            return $"ORD-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString().Substring(0, 8)}";
        }
    }
}
