using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Models.Orders
{
    public class GetOrderDetail
    {
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Guid ProductVariationId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Guid ProductId { get; set; }
        public JsonDocument Attribute { get; set; }
    }
}
