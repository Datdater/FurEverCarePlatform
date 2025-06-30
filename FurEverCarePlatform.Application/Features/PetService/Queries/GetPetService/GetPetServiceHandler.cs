using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService
{
    public class GetPetServiceHandler : IRequestHandler<GetPetServiceQuery, PetServiceDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPetServiceHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PetServiceDto> Handle(
            GetPetServiceQuery request,
            CancellationToken cancellationToken
        )
        {
            var petService = await _unitOfWork
                .GetRepository<Domain.Entities.PetService>()
                .GetFirstOrDefaultAsync(
                    p => p.Id == request.Id,
                    includeProperties: "PetServiceDetails,PetServiceSteps,Store"
                );

            if (petService != null)
            {
                petService.PetServiceDetails = petService
                    .PetServiceDetails.Where(d => d.PetServiceId == request.Id && !d.IsDeleted)
                    .ToList();

                petService.PetServiceSteps = petService
                    .PetServiceSteps.OrderBy(s => s.Priority)
                    .Where(s => s.PetServiceId == request.Id && !s.IsDeleted)
                    .ToList();
            }

            var data = _mapper.Map<PetServiceDto>(petService);
            data.BasePrice = (float)(petService?.PetServiceDetails.Min(x => x.Amount));
            data.StoreCity = petService?.Store.BusinessAddressProvince ?? string.Empty;
            data.StoreDistrict = petService?.Store.BusinessAddressDistrict ?? string.Empty;
            data.StoreId = petService?.StoreId ?? Guid.Empty;
            return data;
        }
    }
}
