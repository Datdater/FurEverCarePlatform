//using FurEverCarePlatform.Domain.Enums;
//using MediatR;
//using Microsoft.Extensions.Configuration;
//using Net.payOS;
//using Net.payOS.Types;
//using Payment.API.DTO;
//using Payment.API.Repository;

//namespace FurEverCarePlatform.Application.Features.Payments.Commands
//{
//    public class CreatePaymentHandler(
//        IUnitOfWork unitOfWork,
//        PayOS payOS,
//        IConfiguration configuration
//    ) : IRequestHandler<CreatePaymentCommand, PaymentCreatedResponse>
//    {
//        public async Task<PaymentCreatedResponse> Handle(
//            CreatePaymentCommand request,
//            CancellationToken cancellationToken
//        )
//        {
//            var paymentUrl = "payment link";
//            string? paymentCode = null;
//            if (request.PaymentMethod == PaymentMethod.BankTransfer)
//            {
//                long orderCode = long.Parse(DateTimeOffset.Now.ToString("mmssffffff"));
//                List<ItemData> items = new List<ItemData>();
//                long expried = DateTimeOffset.Now.ToUnixTimeSeconds() + 15 * 60;
//                PaymentData paymentData = new PaymentData(
//                    orderCode,
//                    (int)request.Amount,
//                    $"Senandpet",
//                    items,
//                    configuration["PayOS:CancelUrl"],
//                    configuration["PayOS:ReturnUrl"],
//                    null,
//                    null,
//                    null,
//                    null,
//                    null,
//                    expried
//                );
//                CreatePaymentResult createPayment = await payOS.createPaymentLink(paymentData);
//                paymentUrl = createPayment.checkoutUrl;
//                paymentCode = createPayment.orderCode.ToString();
//            }
//            payment.PaymentCode = paymentCode;
//            await paymentRepository.AddAsync(payment);
//            var paymentResponse = new PaymentCreatedResponse
//            {
//                Id = payment.Id,
//                PaymentUrl = paymentUrl,
//            };
//            return paymentResponse;
//        }
//    }
//}
