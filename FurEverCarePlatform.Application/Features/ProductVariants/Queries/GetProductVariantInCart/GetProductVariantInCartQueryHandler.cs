using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.ProductVariants.Queries.GetProductVariantInCart
{
    public class GetProductVariantInCartQueryHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetProductVariantInCartQuery, ProductVariantInCartResponse>
    {
        public async Task<ProductVariantInCartResponse> Handle(
            GetProductVariantInCartQuery request,
            CancellationToken cancellationToken
        )
        {
            var productVariant = await unitOfWork
                .GetRepository<Domain.Entities.ProductVariant>()
                .GetQueryable()
                .Include(x => x.Product)
                .Include(x => x.Product.Images)
                .Include(x => x.Product.Store)
                .FirstOrDefaultAsync(x => x.Id == request.ProductVariantId);
            var productVariantInCartResponse = new ProductVariantInCartResponse()
            {
                ProductVariantId = productVariant.Id,
                UnitPrice = productVariant.Price,
                productName = productVariant.Product.Name,
                Attributes = productVariant.Attributes,
                PictureUrl = productVariant.Product.Images.FirstOrDefault(x => x.IsMain)?.ImageUrl,
                StoreId = productVariant.Product.Store.Id,
                StoreName = productVariant.Product.Store.Name,
                StoreUrl = productVariant.Product.Store.LogoUrl,
            };
            return productVariantInCartResponse;
        }
    }
}
