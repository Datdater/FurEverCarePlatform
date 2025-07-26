using FurEverCarePlatform.Application.Features.Payments.Commands;
using FurEverCarePlatform.Application.Features.Payments.Queries.GetAllTransaction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    public class PaymentController(IMediator mediator) : BaseControllerApi
    {
        [HttpPut]
        public async Task<IActionResult> ExcutePayment(SetPaymentCompletedCommand query)
        {
            await mediator.Send(query);
            return Ok();
        }
        [HttpGet("transactions")]
        public async Task<IActionResult> GetTransactions([FromQuery] GetAllTransactionQuery query)
        {
           var transactions =  await mediator.Send(query);
           return Ok(transactions);
        }
    }
}
