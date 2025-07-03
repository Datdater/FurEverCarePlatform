using FurEverCarePlatform.Application.Features.Payments.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    public class PaymentController(IMediator mediator) : Controller
    {
        [HttpPut]
        public async Task<IActionResult> ExcutePayment(SetPaymentCompletedCommand query)
        {
            await mediator.Send(query);
            return Ok();
        }
    }
}
