using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetAllProductByStore;

public class GetAllProductByStoreHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
    : IRequestHandler<GetAllProductByStoreQuery, Pagination<ProductDTO>>
{
    public async Task<Pagination<ProductDTO>> Handle(
        GetAllProductByStoreQuery request,
        CancellationToken cancellationToken
    )
    {
        var userId = claimService.GetCurrentUser;

        var store = await unitOfWork.GetRepository<Domain.Entities.Store>().GetQueryable().FirstOrDefaultAsync(x => x.AppUserId == userId);
        if (store == null) throw new System.Exception("Not found store with this user");
        var product = unitOfWork
            .GetRepository<Product>()
            .GetQueryable()
            .Include(x => x.Store)
            .Include(x => x.Images)
            .Include(x => x.Category)
            .Where(x => !x.IsDeleted && x.StoreId == store.Id);
        var productRaw = await Pagination<Product>.CreateAsync(
            product,
            request.PageNumber,
            request.PageSize
        );
        var productDTOs = mapper.Map<Pagination<ProductDTO>>(productRaw);
        return productDTOs;
    }
}
