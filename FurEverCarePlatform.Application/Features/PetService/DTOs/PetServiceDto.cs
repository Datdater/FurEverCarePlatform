using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace FurEverCarePlatform.Application.Features.PetService.DTOs
{
    public class PetServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
        public string StoreName { get; set; } = string.Empty;
        public Guid ServiceCategoryId { get; set; }
        public string ServiceCategoryName { get; set; } = string.Empty;
        public DateTime EstimatedTime { get; set; }
        public List<PetServiceStepDto> PetServiceSteps { get; set; } = new List<PetServiceStepDto>();
        public List<PetServiceDetailDto> PetServiceDetails { get; set; } = new List<PetServiceDetailDto>(); // Thêm danh sách PetServiceDetails
        public List<ComboServiceDto> ComboServices { get; set; } = new List<ComboServiceDto>();
    }
}
