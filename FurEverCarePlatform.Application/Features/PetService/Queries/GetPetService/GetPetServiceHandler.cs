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

		public async Task<PetServiceDto> Handle(GetPetServiceQuery request, CancellationToken cancellationToken)
		{
			var petService = await _unitOfWork.PetServiceRepository.GetPetService(request.Id);
			var data = _mapper.Map<PetServiceDto>(petService);
			return data;
		}
	}
}
