using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetStoreDetail;

public class GetStoreSpecificHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetStoreSpecificQuery, StoreSpecificDTO>
{
    public async Task<StoreSpecificDTO> Handle(
        GetStoreSpecificQuery request,
        CancellationToken cancellationToken
    )
    {
        var store = await unitOfWork
            .GetRepository<Domain.Entities.Store>()
            .GetFirstOrDefaultAsync(s => s.Id == request.Id);
        var storeSpecific = mapper.Map<StoreSpecificDTO>(store);
        return storeSpecific;
    }
}
