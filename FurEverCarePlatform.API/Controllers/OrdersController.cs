using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Feature.Orders.Commands;

namespace FurEverCarePlatform.API.Controllers
{
    public class OrdersController(IMediator mediator) : BaseControllerApi
    {
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrder)
        {
            await mediator.Send(createOrder);
            return Ok();
        }
        //[HttpGet("{id:guid}")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //public async Task<IActionResult> GetOrderById(Guid id)
        //{
        //    var order = await mediator.Send(new GetOrderByIdQuery { Id = id });
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(order);
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetAllOrders([FromQuery] GetAllOrdersQuery query)
        //{
        //    var orders = await mediator.Send(query);
        //    return Ok(orders);
        //}
        //[HttpPut("{id:guid}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> UpdateOrder(UpdateOrderCommand updateOrder)
        //{
        //    await mediator.Send(updateOrder);
        //    return NoContent();
        //}
        //[HttpDelete("{id:guid}")]
        //[ProducesResponseType(204)]
        //[ProducesResponseType(400)]
        //public async Task<IActionResult> DeleteOrder(Guid id)
        //{
        //    await mediator.Send(new DeleteOrderCommand { Id = id });
        //    return NoContent();
        //}
    }
}
