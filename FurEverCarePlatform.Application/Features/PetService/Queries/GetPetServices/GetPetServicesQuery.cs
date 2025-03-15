using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices	
{
    public class GetPetServicesQuery : IRequest<Pagination<PetServicesDto>>
	{
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
	}
}
