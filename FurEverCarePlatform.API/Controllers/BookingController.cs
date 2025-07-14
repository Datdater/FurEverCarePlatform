using FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking;
using FurEverCarePlatform.Application.Features.Booking.Commands.UpdateBooking;
using FurEverCarePlatform.Application.Features.Booking.Queries.GetAllBookingByUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    public class BookingController : BaseControllerApi
    {
        private readonly IMediator _mediator;

        public BookingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> CreateBooking(CreateBookingCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdateBooking(
            [FromRoute] Guid id,
            [FromBody] UpdateBookingCommand command
        )
        {
            command.Id = id;
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        [Authorize]
        public async Task<IActionResult> GetBookings([FromQuery] GetAllBookingByUserQuery query)
        {
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
