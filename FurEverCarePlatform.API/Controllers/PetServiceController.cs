using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetServiceController : ControllerBase
    {
	    private readonly IMediator _mediator;
		public PetServiceController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[ProducesResponseType(201)]
		[ProducesResponseType(400)]
		[ProducesResponseType(500)]
		public async Task<IActionResult> CreatePetService(CreatePetServiceCommand command)
		{
			var result = await _mediator.Send(command);
			return Ok(result);
		}
	}
}
