using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.ServiceCategory.Queries.GetServiceCategories
{
    public class GetServiceCategoriesHandler : IRequestHandler<GetServiceCategoriesQuery, List<ServiceCategoriesDto>>
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public GetServiceCategoriesHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<List<ServiceCategoriesDto>> Handle(GetServiceCategoriesQuery request, CancellationToken cancellationToken)
		{
			var serviceCategories = await _unitOfWork.GetRepository<Domain.Entities.ServiceCategory>().GetAllAsync();
			var data = _mapper.Map<List<ServiceCategoriesDto>>(serviceCategories);
			return data;
		}
	}
}
