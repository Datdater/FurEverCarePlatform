using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetService;
using FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService;
using FurEverCarePlatform.Application.Features.PetService.DTOs;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetAllPetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //   public class PetServiceController : ControllerBase
    //   {
    //    private readonly IMediator _mediator;
    //	public PetServiceController(IMediator mediator)
    //	{
    //		_mediator = mediator;
    //	}

    //	[HttpPost]
    //	[ProducesResponseType(201)]
    //	[ProducesResponseType(400)]
    //	[ProducesResponseType(500)]
    //	public async Task<IActionResult> CreatePetService(CreatePetServiceCommand command)
    //	{
    //		var result = await _mediator.Send(command);
    //		return Ok(result);
    //	}
    //}

    public class PetServiceController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Post(CreatePetServiceCommand createPetService)
        {
            var response = await mediator.Send(createPetService);
            return CreatedAtRoute(
                "GetPetServiceById",
                new { id = response },
                response
            );
        }

        [HttpGet]
        public async Task<Pagination<PetServiceDto>> GetPetServices([FromQuery] GetAllPetServiceQuery query)
        {
            return await mediator.Send(query);
        }

        [HttpGet("{id}", Name = "GetPetServiceById")]
        public async Task<PetServiceDto> GetPetServiceById(Guid id)
        {
            return await mediator.Send(new GetPetServiceQuery(id));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletePetService(Guid id)
        {
            await mediator.Send(new DeletePetServiceCommand(id));
            return NoContent();
        }

        //[HttpPut("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> UpdatePetService(UpdatePetServiceCommand updatePetService)
        //{
        //    await mediator.Send(updatePetService);
        //    return NoContent();
        //}
    }
}
