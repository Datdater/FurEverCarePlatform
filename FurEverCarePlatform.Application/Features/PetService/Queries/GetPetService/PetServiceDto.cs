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
        public float BasePrice { get; set; }
        public string StoreCity { get; set; }
        public string StoreDistrict { get; set; }
        public int TotalUsed { get; set; } = 10;
        public int RatingAverage { get; set; } = 5;
        public int TotalReviews { get; set; } = 5;
        public string Image { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
        public string? EstimatedTime { get; set; }
        public Guid ServiceCategoryId { get; set; }

        public string ServiceCategoryName { get; set; }

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
        public string Image { get; set; } = string.Empty;

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
