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
        public string? Image { get; set; }
        public Guid StoreId { get; set; }
        public string? EstimatedTime { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public float BasePrice { get; set; }
        public string StoreCity { get; set; }
        public string StoreDistrict { get; set; }
        public int TotalUsed { get; set; }
        public int TotalReviews { get; set; }
        public float RatingAverage { get; set; }
        public bool Status { get; set; }

        public string StoreName { get; set; }

        public string CategoryName { get; set; }
    }
}
