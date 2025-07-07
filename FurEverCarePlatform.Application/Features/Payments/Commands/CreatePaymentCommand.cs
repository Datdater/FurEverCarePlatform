using FurEverCarePlatform.Domain.Enums;
using MediatR;
using Payment.API.DTO;

namespace FurEverCarePlatform.Application.Features.Payments.Commands
{
    public class CreatePaymentCommand : IRequest<PaymentCreatedResponse>
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
