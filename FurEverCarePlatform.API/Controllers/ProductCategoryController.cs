using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory;
using FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ProductCategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    public ProductCategoryController(IMediator mediator)
    {
        this._mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Post(CreateProductCategoryCommand createCategory)
    {
        var response = await _mediator.Send(createCategory);
        return CreatedAtRoute(
            "GetCategoryById",
            new { id = response }
        );
    }

    [HttpGet]
    public async Task<Pagination<ProductCategoryDto>> GetCategories([FromQuery] GetProductCategoryQuery query)
    {
        return await _mediator.Send(query);
    }

}