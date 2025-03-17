using FurEverCarePlatform.Application.Features.ServiceCategory.Queries.GetServiceCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.MappingProfile
{
    public class ServiceCategoryProfile : Profile
	{
		public ServiceCategoryProfile()
		{
			CreateMap<ServiceCategoriesDto, ServiceCategory>().ReverseMap();
		}
	}
}
