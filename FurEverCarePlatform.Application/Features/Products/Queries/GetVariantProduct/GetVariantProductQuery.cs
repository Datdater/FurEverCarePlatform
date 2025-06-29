using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetVariantProduct
{
    public class GetVariantProductQuery : IRequest<ProductVariantResponseDTO>
    {
        public JsonDocument Attribute { get; set; }
        public string ProductId { get; }

        public GetVariantProductQuery(string productId)
        {
            ProductId = productId;
        }
    }

    public class ProductVariantResponseDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public JsonDocument Attributes { get; set; }
        public float Price { get; set; }
        public int Stock { get; set; }
    }
}
