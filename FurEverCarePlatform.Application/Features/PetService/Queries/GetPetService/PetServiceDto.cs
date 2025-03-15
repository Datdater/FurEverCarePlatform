using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService
{
    public class PetServiceDto
    {
		public Guid? Id { get; set; }
	    public string? Name { get; set; }
	    public string? Description { get; set; }
	    public Guid StoreId { get; set; }
	    public string? EstimatedTime { get; set; }
	    public Guid ServiceCategoryId { get; set; }

		public bool Status { get; set; }
		public List<PetServiceDetailDto> PetServiceDetails { get; set; }

	    public List<PetServiceStepDto> PetServiceSteps { get; set; }
    }

    public class PetServiceDetailDto
    {
	    public Guid Id { get; set; }
	    public float? PetWeightMin { get; set; }
	    public float? PetWeightMax { get; set; }
	    public float? Amount { get; set; }
	    public bool PetType { get; set; }
	    public string? Description { get; set; }
	    public string? Name { get; set; }
    }

    public class PetServiceStepDto
    {
	    public Guid Id { get; set; }
	    public string? Name { get; set; }
	    public string? Description { get; set; }
	    public int Priority { get; set; }
    }
}
