using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.Product.Commands.CreateProduct;
using FurEverCarePlatform.Application.Features.Product.Commands.DeleteProduct;
using FurEverCarePlatform.Application.Features.Product.Commands.UpdateProduct;
using FurEverCarePlatform.Application.Features.Product.DTOs;
using FurEverCarePlatform.Application.Features.Product.Queries.GetAllProduct;
using FurEverCarePlatform.Application.Features.Product.Queries.GetProductDetail;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
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

        [HttpGet("{id}", Name = "GetProductById")]
        public async Task<ProductSpecificDTO> GetProductById(Guid id)
        {
            return await mediator.Send(new GetProductSpecificQuery { Id = id });
        }

        [HttpDelete("{id}")]
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
    }
}
