using System.Net;
using System.Security.Claims;
using FurEverCarePlatform.Application.Contracts;
using FurEverCarePlatform.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FurEverCarePlatform.API.Controllers
{
    public class CartController : BaseControllerApi
    {
        private readonly ICartRepository _repository;

        public CartController(ICartRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetCart(string userId)
        {
            var cart = await _repository.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost()]
        [Authorize]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateCart(
            [FromBody] UpdateCartItemRequest request
        )
        {
            var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = await _repository.GetCartAsync(currentUserId);
            cart.UpdateItemQuantity(request.Id, request.Quantity);
            await _repository.UpdateCartAsync(cart);
            return Ok(await _repository.GetCartAsync(cart.UserId));
        }

        [HttpPost("items")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> AddItemToCart(
            [FromBody] AddCartItemRequest request
        )
        {
            var cart = await _repository.GetCartAsync(request.UserId);
            cart.AddItem(
                request.ProductVariantId,
                request.ProductName,
                request.UnitPrice,
                request.Quantity,
                request.PictureUrl,
                request.Attributes,
                request.StoreId,
                request.StoreUrl,
                request.StoreName
            );
            await _repository.UpdateCartAsync(cart);

            return Ok(cart);
        }

        [HttpDelete("{userId}/items/{id}")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> RemoveItemFromCart(string userId, string id)
        {
            var cart = await _repository.GetCartAsync(userId);
            cart.RemoveItem(id);
            await _repository.UpdateCartAsync(cart);

            return Ok(cart);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ClearCart(string userId)
        {
            await _repository.DeleteCartAsync(userId);
            return Ok();
        }
    }
}
