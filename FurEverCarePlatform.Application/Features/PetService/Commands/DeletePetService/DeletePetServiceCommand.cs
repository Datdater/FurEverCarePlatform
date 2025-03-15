using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetService
{
    public class DeletePetServiceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
