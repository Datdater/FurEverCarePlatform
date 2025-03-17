using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Queries.GetAllProduct;

public class GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllProductQuery, Pagination<ProductDTO>>
{
    public async Task<Pagination<ProductDTO>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var productRaw = await unitOfWork.GetRepository<Domain.Entities.Product>()
            .GetPaginationAsync("ProductCategory,Store,ProductBrand", request.PageNumber, request.PageSize);
        var productDTOs = mapper.Map<Pagination<ProductDTO>>(productRaw);
        var productDTOItem = productDTOs.Items.Select(p => new ProductDTO
        {
            Id = p.Id,
            Name = p.Name,
            ProductCode = p.ProductCode,
            BrandName = p.BrandName,
            CategoryName = p.CategoryName,
            StoreName = p.StoreName,
            MinPrices = unitOfWork.ProductRepository.GetMinProductPrice(p.Id)
        }).ToList();
        productDTOs.Items = productDTOItem;
        return productDTOs;
    }
}
