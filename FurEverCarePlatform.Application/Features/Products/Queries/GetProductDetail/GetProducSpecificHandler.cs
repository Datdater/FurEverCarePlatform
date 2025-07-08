using FurEverCarePlatform.Application.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetProductDetail;

public class GetProducSpecificHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetProductSpecificQuery, ProductSpecificDTO>
{
    public async Task<ProductSpecificDTO> Handle(
        GetProductSpecificQuery request,
        CancellationToken cancellationToken
    )
    {
        var productDetail = await unitOfWork
            .GetRepository<Domain.Entities.Product>()
            .GetQueryable()
            .Include(x => x.Store)
            .Include(x => x.Variants)
            .Include(x => x.Images)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        ;
        var productSpecificDTO = new ProductSpecificDTO()
        {
            Id = productDetail.Id,
            Name = productDetail.Name,
            StoreId = productDetail.StoreId,
            StoreName = productDetail.Store.Name,
            StoreUrl = productDetail.Store.LogoUrl,
            Description = productDetail.Description,
            CategoryName = productDetail.Category.Name,
            Height = productDetail.Height,
            Length = productDetail.Length,
            ReviewCount = productDetail.ReviewCount,
            Sold = productDetail.Sold,
            StarAverage = productDetail.StarAverage,
            Weight = productDetail.Weight,
            Width = productDetail.Width,
            Variants = productDetail
                .Variants.Select(v => new ProductVariantDTO
                {
                    Id = v.Id,
                    Attributes = v.Attributes,
                    Price = v.Price,
                    Stock = v.Stock,
                })
                .ToList(),
            Images = productDetail
                .Images.Select(i => new ProductImageDTO
                {
                    IsMain = i.IsMain,
                    ImageUrl = i.ImageUrl,
                })
                .ToList(),
        };
        return productSpecificDTO;
    }
}
