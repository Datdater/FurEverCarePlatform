using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices
{
    public class GetPetServicesHandler : IRequestHandler<GetPetServicesQuery, Pagination<PetServicesDto>>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
        private readonly Guid userId;
		public GetPetServicesHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService service)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
            userId = service.GetCurrentUser;
		}
		public async Task<Pagination<PetServicesDto>> Handle(GetPetServicesQuery request, CancellationToken cancellationToken)
		{
			var petService = await _unitOfWork.GetRepository<Domain.Entities.PetService>().GetPaginationAsync(s => !s.IsDeleted && s.Store.UserId == userId, includeProperties: "Store,ServiceCategory", request.PageIndex, request.PageSize);
			var data = _mapper.Map<Pagination<PetServicesDto>>(petService);
			return data;
		}
	}
}
