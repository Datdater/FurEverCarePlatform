using System.Text.Json;

namespace FurEverCarePlatform.Application.Models
{
    public class CartItem
    {
        public string Id { get; private set; }
        public string ProductVariantId { get; private set; }
        public string Attributes { get; set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public string PictureUrl { get; private set; }
        public string StoreId { get; set; } // Optional, can be used to associate the item with a specific store
        public string StoreUrl { get; set; } // Optional, can be used to provide a URL for the store
        public string StoreName { get; set; }

        public CartItem(
            string productId,
            string productName,
            decimal unitPrice,
            int quantity,
            string pictureUrl,
            string attribute,
            string storeId,
            string storeUrl,
            string storeName
        )
        {
            Id = Guid.NewGuid().ToString();
            ProductVariantId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            PictureUrl = pictureUrl;
            Attributes = attribute;
            StoreId = storeId;
            StoreUrl = storeUrl;
            StoreName = storeName;
        }

        public void UpdateQuantity(int quantity)
        {
            Quantity = quantity;
        }
    }
}
