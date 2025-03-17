using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.Store.Commands.CreateStore;
using FurEverCarePlatform.Application.Features.Store.Commands.DeleteStore;
using FurEverCarePlatform.Application.Features.Store.Commands.UpdateStore;
using FurEverCarePlatform.Application.Features.Store.DTOs;
using FurEverCarePlatform.Application.Features.Store.Queries.GetAllStores;
using FurEverCarePlatform.Application.Features.Store.Queries.GetStoreAddress;
using FurEverCarePlatform.Application.Features.Store.Queries.GetStoreDetail;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers;

[Route("/api/v1/[controller]")]
[ApiController]
public class StoreController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<Pagination<StoreDTO>> GetStores([FromQuery] GetAllStoresQuery query)
    {
        return await mediator.Send(query);
    }

    [HttpGet("{id}", Name = "GetStoreById")]
    public async Task<StoreSpecificDTO> GetStoreById(Guid id)
    {
        return await mediator.Send(new GetStoreSpecificQuery { Id = id });
    }

    [HttpGet(template: "addresses")]
    public async Task<IEnumerable<StoreAddressDTO>> GetUserAddress(
        [FromQuery] GetStoreAddressQuery query
    )
    {
        return await mediator.Send(query);
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Guid>> Post(CreateStoreCommand createStore)
    {
        var response = await mediator.Send(createStore);
        return Created();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> UpdateStore(UpdateStoreCommand updateStore)
    {
        await mediator.Send(updateStore);
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> DeleteStore(Guid id)
    {
        await mediator.Send(new DeleteStoreCommand { Id = id });
        return NoContent();
    }
}
