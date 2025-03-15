using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers;

[Route("api/v1/[controller]")]
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

	[HttpGet("{id}")]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(500)]

	public async Task<IActionResult> GetPetService(Guid id)
	{
		GetPetServiceQuery query = new GetPetServiceQuery { Id = id };
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpPut("{id}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> UpdatePetService(Guid id, UpdatePetServiceCommand command)
	{
		if (id != command.Id)
		{
			return BadRequest();
		}
		await _mediator.Send(command);
		return NoContent();
	}

	[HttpGet]
	[ProducesResponseType(200)]
	[ProducesResponseType(400)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> GetAllPetServices([FromQuery] GetPetServicesQuery query)
	{
		var result = await _mediator.Send(query);
		return Ok(result);
	}

	[HttpDelete("{id}")]
	[ProducesResponseType(204)]
	[ProducesResponseType(400)]
	[ProducesResponseType(500)]
	public async Task<IActionResult> DeletePetService(Guid id)
	{
		GetPetServiceQuery query = new GetPetServiceQuery { Id = id };
		await _mediator.Send(query);
		return NoContent();
	}
}
