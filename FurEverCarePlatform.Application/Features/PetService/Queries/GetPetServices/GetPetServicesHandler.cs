//using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices
//{
//    public class GetPetServicesHandler : IRequestHandler<GetPetServicesQuery, Pagination<PetServicesDto>>
//	{
//		private readonly IUnitOfWork _unitOfWork;
//		private readonly IMapper _mapper;
//		public GetPetServicesHandler(IUnitOfWork unitOfWork, IMapper mapper)
//		{
//			_unitOfWork = unitOfWork;
//			_mapper = mapper;
//		}
//		public async Task<Pagination<PetServicesDto>> Handle(GetPetServicesQuery request, CancellationToken cancellationToken)
//		{
//			var petService = await _unitOfWork.GetRepository<Domain.Entities.PetService>().GetPaginationAsync(s => !s.IsDeleted ,null, request.PageIndex, request.PageSize);
//			var data = _mapper.Map<Pagination<PetServicesDto>>(petService);
//			return data;
//		}
//	}
//}
