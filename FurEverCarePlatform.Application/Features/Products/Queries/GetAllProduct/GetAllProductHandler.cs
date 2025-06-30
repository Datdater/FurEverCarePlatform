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
            .GetRepository<Domain.Entities.Product>()
            .GetQueryable()
            .Include(x => x.Store)
            .Include(x => x.Images)
            .Where(x => !x.IsDeleted);
        if (!string.IsNullOrEmpty(request.SearchTerm))
        {
            product = product.Where(x => x.Name.Contains(request.SearchTerm));
        }
        if (!string.IsNullOrEmpty(request.SortBy))
        {
            product = request.SortBy switch
            {
                "name" => product.OrderBy(x => x.Name),
                "price" => product.OrderBy(x => x.BasePrice),
                _ => product.OrderBy(x => x.Name),
            };
        }

        var productRaw = await Pagination<Domain.Entities.Product>.CreateAsync(
            product,
            request.PageNumber,
            request.PageSize
        );
        var productDTOs = mapper.Map<Pagination<ProductDTO>>(productRaw);
        return productDTOs;
    }
}
