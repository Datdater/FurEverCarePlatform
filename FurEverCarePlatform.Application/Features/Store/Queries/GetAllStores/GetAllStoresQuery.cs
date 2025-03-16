using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetAllStores;

public class GetAllStoresQuery : IRequest<Pagination<StoreDTO>>
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 5;
}
