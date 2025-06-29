using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetListServiceCategory
{
	public class GetListServiceCategoryQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetListServiceCategoryQuery, List<ServiceCategoryDto>>
	{

		public async Task<List<ServiceCategoryDto>> Handle(GetListServiceCategoryQuery request, CancellationToken cancellationToken)
		{
			var serviceCategories = await unitOfWork.GetRepository<Domain.Entities.ServiceCategory>().GetAllAsync();
			return serviceCategories
				.Select(sc => new ServiceCategoryDto
				{
					Id = sc.Id,
					Name = sc.Name
				})
				.ToList();
		}
	}
}
