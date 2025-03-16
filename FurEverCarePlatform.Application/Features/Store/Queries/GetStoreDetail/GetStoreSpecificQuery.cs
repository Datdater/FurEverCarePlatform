using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetStoreDetail;

public class GetStoreSpecificQuery : IRequest<StoreSpecificDTO>
{
    public Guid Id { get; set; }
}
