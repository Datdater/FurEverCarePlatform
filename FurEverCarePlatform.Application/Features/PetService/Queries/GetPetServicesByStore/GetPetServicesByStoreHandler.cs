using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServicesByStore
{
    public class GetPetServicesByStoreHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService) : IRequestHandler<GetPetServicesByStoreQuery, Pagination<PetServicesDto>>
    {
        public async Task<Pagination<PetServicesDto>> Handle(GetPetServicesByStoreQuery request, CancellationToken cancellationToken)
        {
            var userId = claimService.GetCurrentUser;

            var store = await unitOfWork.GetRepository<Domain.Entities.Store>().GetQueryable().FirstOrDefaultAsync(x => x.AppUserId == userId);
            if (store == null) throw new System.Exception("Not found store with this user");

            var petService = unitOfWork
                .GetRepository<Domain.Entities.PetService>()
                .GetQueryable()
                .Include(x => x.Store)
                .Where(x => !x.IsDeleted && x.StoreId == store.Id);

            var petServicePagination = await Pagination<Domain.Entities.PetService>.CreateAsync(
                petService,
                request.PageIndex,
                request.PageSize
            );
            var data = mapper.Map<Pagination<PetServicesDto>>(petServicePagination);
            return data;
        }
    }
}
