using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.Products.Commands.CreateProduct;
using FurEverCarePlatform.Application.Features.Products.Commands.DeleteProduct;
using FurEverCarePlatform.Application.Features.Products.Commands.UpdateProduct;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using FurEverCarePlatform.Application.Features.Products.Queries.GetAllProduct;
using FurEverCarePlatform.Application.Features.Products.Queries.GetAllProductByStore;
using FurEverCarePlatform.Application.Features.Products.Queries.GetProductDetail;
using FurEverCarePlatform.Application.Features.Products.Queries.GetProductReviews;
using FurEverCarePlatform.Application.Features.Products.Queries.GetVariantProduct;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Authorize]
        public async Task<ActionResult> Post(CreateProductCommand createProduct)
        {
            var response = await mediator.Send(createProduct);
            return Ok();
        }

        [HttpGet]
        public async Task<Pagination<ProductDTO>> GetProducts([FromQuery] GetAllProductQuery query)
        {
            return await mediator.Send(query);
        }
        [HttpGet("my-store")]
        [Authorize]
        public async Task<Pagination<ProductDTO>> GetProductsByStore([FromQuery] GetAllProductByStoreQuery query)
        {
            return await mediator.Send(query);
        }

        [HttpGet("{id:guid}", Name = "GetProductById")]
        public async Task<ProductSpecificDTO> GetProductById(Guid id)
        {
            return await mediator.Send(new GetProductSpecificQuery { Id = id });
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            await mediator.Send(new DeleteProductCommand { Id = id });
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProduct)
        {
            await mediator.Send(updateProduct);
            return NoContent();
        }

        [HttpGet("{id}/reviews")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductReviews(Guid id)
        {
            // Assuming you have a query to get product reviews
            var reviews = await mediator.Send(new GetProductReviewsQuery { ProductId = id });
            if (reviews == null)
            {
                return NotFound();
            }
            return Ok(reviews);
        }

        [HttpPost("{id}/product-variants")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductVariants([FromBody] GetVariantProductQuery query)
        {
            // Assuming you have a query to get product variants
            var variants = await mediator.Send(query);
            if (variants == null)
            {
                return NotFound();
            }
            return Ok(variants);
        }
    }
}
