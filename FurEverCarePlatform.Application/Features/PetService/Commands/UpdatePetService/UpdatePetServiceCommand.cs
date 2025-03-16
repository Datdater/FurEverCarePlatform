using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService
{
    public record UpdatePetServiceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Guid StoreId { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public DateTime EstimatedTime { get; set; }
    }
}
