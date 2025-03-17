using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetServiceStep
{
   public class DeletePetServiceStepCommand : IRequest<Guid>
	{
		public Guid Id { get; set; }
	}
}
