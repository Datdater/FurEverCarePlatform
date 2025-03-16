using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.DTOs
{
    public class ComboServiceDto
    {
        public Guid Id { get; set; } // Có thể dùng Id từ BaseEntity
        public Guid ComboId { get; set; }
        public Guid PetServiceId { get; set; }
        public string ComboName { get; set; } = string.Empty; // Thêm tên từ quan hệ Combo
    }
}
