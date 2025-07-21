using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreateReview;
using FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetService;
using FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetServiceDetail;
using FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetServiceStep;
using FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServicesByStore;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetServiceReviews;
using FurEverCarePlatform.Application.Features.Products.Commands.CreateProductReviews;
using FurEverCarePlatform.Application.Features.Products.Queries.GetProductReviews;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FurEverCarePlatform.API.Controllers;

[Route("api/v1/services")]
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
    [HttpPost("{id}/reviews")]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    [Authorize]
    public async Task<IActionResult> CreatePetServiceReview([FromRoute] Guid id,CreateServiceReviewCommand command)
    {
        command.PetServiceId = id;
        await _mediator.Send(command);
        return Ok();
    }
    [HttpGet("{id}/reviews")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetServiceReviews(Guid id)
    {
        // Assuming you have a query to get product reviews
        var reviews = await _mediator.Send(new GetServiceReviewQuery { Id = id });
        if (reviews == null)
        {
            return NotFound();
        }
        return Ok(reviews);
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
    [HttpGet("my-store")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> GetAllPetServicesByStore([FromQuery] GetPetServicesByStoreQuery query)
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
        DeletePetServiceCommand query = new DeletePetServiceCommand { Id = id };
        await _mediator.Send(query);
        return NoContent();
    }

    [HttpDelete("{id}/service-details/{detailId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeletePetServiceDetail(Guid id, Guid detailId)
    {
        DeletePetServiceDetailCommand query = new DeletePetServiceDetailCommand { Id = detailId };
        await _mediator.Send(query);
        return NoContent();
    }

    [HttpDelete("{id}/service-steps/{stepId}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> DeletePetServiceStep(Guid id, Guid stepId)
    {
        DeletePetServiceStepCommand query = new DeletePetServiceStepCommand { Id = stepId };
        await _mediator.Send(query);
        return NoContent();
    }
}
