using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.MappingProfile;

public class StoreProfile : Profile
{
    public StoreProfile()
    {
        CreateMap<Store, StoreDTO>().ReverseMap();
        CreateMap<Pagination<Store>, Pagination<StoreDTO>>().ReverseMap();
        CreateMap<Store, StoreSpecificDTO>().ReverseMap();
    }
}
