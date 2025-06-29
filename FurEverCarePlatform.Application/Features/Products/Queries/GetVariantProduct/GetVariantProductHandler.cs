using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetVariantProduct
{
    public class GetVariantProductHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetVariantProductQuery, ProductVariantResponseDTO>
    {
        public async Task<ProductVariantResponseDTO> Handle(
            GetVariantProductQuery request,
            CancellationToken cancellationToken
        )
        {
            var variantProduct = await unitOfWork
                .GetRepository<ProductVariant>()
                .GetQueryable()
                .FirstOrDefaultAsync(x => x.Attributes == request.Attribute);
            var response = new ProductVariantResponseDTO
            {
                Id = variantProduct.Id,
                ProductId = variantProduct.ProductId,
                Attributes = variantProduct.Attributes,
                Price = variantProduct.Price,
                Stock = variantProduct.Stock,
            };
            return response;
        }
    }
}
