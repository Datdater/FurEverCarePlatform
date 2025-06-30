using FurEverCarePlatform.Application.Features.Pets.Commands.CreatePet;
using FurEverCarePlatform.Application.Features.Pets.Commands.DeletePet;
using FurEverCarePlatform.Application.Features.Pets.Commands.UpdatePet;
using FurEverCarePlatform.Application.Features.Pets.Queries.GetAllPets;
using FurEverCarePlatform.Application.Features.Pets.Queries.GetPetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> CreatePet(CreatePetCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPet(Guid id)
        {
            GetPetByIdQuery query = new GetPetByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetPets()
        {
            GetAllPetsQuery query = new GetAllPetsQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> UpdatePetService(Guid id, UpdatePetCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            await _mediator.Send(command);
            return Ok();
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> DeletePet(Guid id)
        {
            DeletePetCommand query = new DeletePetCommand { Id = id };
            await _mediator.Send(query);
            return Ok();
        }
    }
}
