using FurEverCarePlatform.Application.Contracts;
using FurEverCarePlatform.Application.Features.Store.DTOs;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetAllStores;

public class GetAllStoresHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : IRequestHandler<GetAllStoresQuery, Pagination<StoreDTO>>
{
    public async Task<Pagination<StoreDTO>> Handle(
        GetAllStoresQuery request,
        CancellationToken cancellationToken
    )
    {
        var storeRaw = await unitOfWork
            .GetRepository<Domain.Entities.Store>()
            .GetPaginationAsync("Promotions,PetServices", request.PageNumber, request.PageSize);
        var storeDTOs = mapper.Map<Pagination<StoreDTO>>(storeRaw);
        var storeDTOItem = storeDTOs
            .Items.Select(s => new StoreDTO
            {
                Id = s.Id,
                Name = s.Name,
                Hotline = s.Hotline,
                LogoUrl = s.LogoUrl,
                BannerUrl = s.BannerUrl,
                BusinessType = s.BusinessType,
                BusinessAddressProvince = s.BusinessAddressProvince,
                BusinessAddressDistrict = s.BusinessAddressDistrict,
                BusinessAddressWard = s.BusinessAddressWard,
                BusinessAddressStreet = s.BusinessAddressStreet,
                FaxEmail = s.FaxEmail,
                FaxCode = s.FaxCode,
                FrontIdentityCardUrl = s.FrontIdentityCardUrl,
                BackIdentityCardUrl = s.BackIdentityCardUrl,
                //Promotions = s.Promotions.Select(p => new PromotionDTO
                //{
                //    Id = p.Id,
                //    Name = p.Name,
                //    Description = p.Description,
                //    Discount = p.Discount,
                //    StartDate = p.StartDate,
                //    EndDate = p.EndDate,
                //    StoreId = p.StoreId,
                //}).ToList(),
                //PetServices = s.PetServices.Select(ps => new PetServiceDTO
                //{
                //    Id = ps.Id,
                //    Name = ps.Name,
                //    Description = ps.Description,
                //    Price = ps.Price,
                //    StoreId = ps.StoreId,
                //}
            })
            .ToList();
        storeDTOs.Items = storeDTOItem;
        return storeDTOs;
    }
}
