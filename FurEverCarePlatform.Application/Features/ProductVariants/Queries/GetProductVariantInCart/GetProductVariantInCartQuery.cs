using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.ProductVariants.Queries.GetProductVariantInCart
{
    public class GetProductVariantInCartQuery : IRequest<ProductVariantInCartResponse>
    {
        public Guid ProductVariantId { get; set; }
    }

    public class ProductVariantInCartResponse
    {
        public Guid ProductVariantId { get; set; }
        public string productName { get; set; }
        public float UnitPrice { get; set; }
        public JsonDocument Attributes { get; set; }
        public string PictureUrl { get; set; }
        public Guid StoreId { get; set; }
        public string StoreName { get; set; }
        public string StoreUrl { get; set; }
    }
}
