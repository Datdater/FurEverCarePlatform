using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices
{
    public class PetServicesDto
    {
		public Guid? Id { get; set; }
	    public string? Name { get; set; }
	    public string? Description { get; set; }
	    public Guid StoreId { get; set; }
	    public string? EstimatedTime { get; set; }
	    public Guid ServiceCategoryId { get; set; }

		public bool Status { get; set; }

        public string StoreName { get; set; }

        public string CategoryName { get; set; }
	}
}
