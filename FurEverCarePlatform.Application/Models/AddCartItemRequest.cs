using System.Text.Json;

namespace FurEverCarePlatform.Application.Models
{
    public class AddCartItemRequest
    {
        public string UserId { get; set; }
        public string ProductVariantId { get; set; }
        public string ProductName { get; set; }
        public string Attributes { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string PictureUrl { get; set; }
        public string StoreId { get; set; } // Optional, can be used to associate the item with a specific store
        public string StoreName { get; set; }
        public string StoreUrl { get; set; } // Optional, can be used to provide a URL for the store
    }
}
