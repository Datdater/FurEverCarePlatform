using System.Text.Json;
using System.Text.Json.Serialization;

namespace FurEverCarePlatform.Application.Models
{
    public class ShoppingCart
    {
        public string UserId { get; private set; }
        public List<CartItem> Items { get; private set; } = new List<CartItem>();

        public ShoppingCart(string userId)
        {
            UserId = userId;
        }

        public CartItem AddItem(
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
            var existingItem = Items.FirstOrDefault(i => i.ProductVariantId == productId);

            if (existingItem != null)
            {
                existingItem.UpdateQuantity(existingItem.Quantity + quantity);
                return existingItem;
            }

            var newItem = new CartItem(
                productId,
                productName,
                unitPrice,
                quantity,
                pictureUrl,
                attribute,
                storeId,
                storeUrl,
                storeName
            );
            Items.Add(newItem);
            return newItem;
        }

        public void RemoveItem(string id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                Items.Remove(item);
            }
        }

        public void UpdateItemQuantity(string id, int quantity)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item != null)
            {
                if (quantity <= 0)
                {
                    RemoveItem(id);
                }
                else
                {
                    item.UpdateQuantity(quantity);
                }
            }
        }

        public void Clear()
        {
            Items.Clear();
        }
    }
}
