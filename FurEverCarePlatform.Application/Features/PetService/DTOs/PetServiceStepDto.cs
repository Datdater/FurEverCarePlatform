using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.DTOs
{
    public class PetServiceStepDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Priority { get; set; } // Đổi từ StepOrder thành Priority để khớp với entity
        public Guid PetServiceId { get; set; }
    }
}
