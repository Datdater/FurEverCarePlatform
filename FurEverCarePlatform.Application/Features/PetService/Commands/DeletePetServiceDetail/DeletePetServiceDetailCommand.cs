using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetServiceDetail
{
    public class DeletePetServiceDetailCommand : IRequest<Guid>
    {
		public Guid Id { get; set; }
	}
}
