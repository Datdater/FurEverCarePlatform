namespace FurEverCarePlatform.Application.Models
{
    public class UpdateCartItemRequest
    {
        public string? UserId { get; set; }
        public string Id { get; set; } // The ID of the cart item to update
        public int Quantity { get; set; } // The new quantity for the cart item

        public UpdateCartItemRequest(string userId, string id, int quantity)
        {
            UserId = userId;
            Id = id;
            Quantity = quantity;
        }
    }
}
