using FurEverCarePlatform.Application.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetAllProduct;

public class GetAllProductHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllProductQuery, Pagination<ProductDTO>>
{
    public async Task<Pagination<ProductDTO>> Handle(
        GetAllProductQuery request,
        CancellationToken cancellationToken
    )
    {
        var product = unitOfWork
            .GetRepository<Product>()
            .GetQueryable()
            .Include(x => x.Store)
            .Where(x => !x.IsDeleted);
        var productRaw = await Pagination<Product>.CreateAsync(
            product,
            request.PageNumber,
            request.PageSize
        );
        var productDTOs = mapper.Map<Pagination<ProductDTO>>(productRaw);
        return productDTOs;
    }
}
