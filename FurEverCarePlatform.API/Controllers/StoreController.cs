using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.Store.DTOs;
using FurEverCarePlatform.Application.Features.Store.Queries.GetAllStores;
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
    //[HttpPost]
    //[ProducesResponseType(201)]
    //[ProducesResponseType(400)]
    //public async Task<ActionResult> Post(CreateStoreCommand createStore)
    //{
    //    var response = await mediator.Send(createStore);
    //    return CreatedAtRoute(
    //        "GetStoreById",
    //        new { id = response }
    //    );
    //}
    //[HttpPut("{id}")]
    //[ProducesResponseType(204)]
    //[ProducesResponseType(400)]
    //public async Task<IActionResult> UpdateStore(UpdateStoreCommand updateStore)
    //{
    //    await mediator.Send(updateStore);
    //    return NoContent();
    //}
    //[HttpDelete("{id}")]
    //[ProducesResponseType(204)]
    //[ProducesResponseType(400)]
    //public async Task<IActionResult> DeleteStore(Guid id)
    //{
    //    await mediator.Send(new DeleteStoreCommand { Id = id });
    //    return NoContent();
    //}
}
