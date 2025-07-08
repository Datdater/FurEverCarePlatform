using FurEverCarePlatform.Application.Features.ProductVariants.Queries.GetProductVariantInCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    public class ProductVariantsController(IMediator mediator) : BaseControllerApi
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductVariants([FromRoute] Guid id)
        {
            var query = new GetProductVariantInCartQuery { ProductVariantId = id };
            var result = await mediator.Send(query);
            return Ok(result);
        }
    }
}
