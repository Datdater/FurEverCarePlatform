using MediatR;

namespace FurEverCarePlatform.Application.Features.Payments.Commands
{
    public record SetPaymentCompletedCommand(Guid? paymentId, string? orderCode) : IRequest;
}
