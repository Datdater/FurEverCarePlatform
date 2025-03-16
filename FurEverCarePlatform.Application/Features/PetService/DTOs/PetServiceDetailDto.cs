using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.DTOs
{
    public class PetServiceDetailDto
    {
        public Guid Id { get; set; }
        public Guid PetServiceId { get; set; }
        public float PetWeightMin { get; set; }
        public float PetWeightMax { get; set; }
        public float Amount { get; set; }
        public bool PetType { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
