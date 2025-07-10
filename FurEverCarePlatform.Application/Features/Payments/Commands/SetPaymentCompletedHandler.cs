using MediatR;
using Microsoft.EntityFrameworkCore;
using Net.payOS;

namespace FurEverCarePlatform.Application.Features.Payments.Commands
{
    public class SetPaymentCompletedHandler(IUnitOfWork unitOfWork, PayOS payOS)
        : IRequestHandler<SetPaymentCompletedCommand>
    {
        public async Task Handle(
            SetPaymentCompletedCommand request,
            CancellationToken cancellationToken
        )
        {
            var payment = await unitOfWork
                .GetRepository<Domain.Entities.Payment>()
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.Id == request.paymentId);

            if (!string.IsNullOrEmpty(request.orderCode))
            {
                //check return status payos
                var checkPayment = await payOS.getPaymentLinkInformation(
                    long.Parse(request.orderCode)
                );
                if (checkPayment.status != "PAID")
                {
                    throw new System.Exception("Payment is not completed");
                }
                payment.PaymentStatus = Domain.Enums.PaymentStatus.Completed;
                var order = await unitOfWork
                    .GetRepository<Domain.Entities.Order>()
                    .GetQueryable()
                    .Include(x => x.Payment)
                    .Include(x => x.OrderDetails)
                    .ThenInclude(x => x.ProductVariation.Product.Store)
                    .FirstOrDefaultAsync(x => x.Payment.Id == payment.Id);
                order.OrderStatus = Domain.Enums.EnumOrderStatus.Confirmed;
                unitOfWork.GetRepository<Domain.Entities.Order>().Update(order);
                unitOfWork.GetRepository<Domain.Entities.Payment>().Update(payment);
                await unitOfWork.SaveAsync();
            }
            //await unitOfWork.UpdateAsync(payment);
            //paymentmethod is bankTransfer
            //push event to order service update to Confirmed
        }
    }
}
