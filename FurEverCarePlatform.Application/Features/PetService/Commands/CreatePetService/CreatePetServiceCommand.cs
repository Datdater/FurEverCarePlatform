using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
	public class CreatePetServiceCommand : IRequest<Guid>
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public Guid StoreId { get; set; }
		public DateTime EstimatedTime { get; set; }
		public Guid ServiceCategoryId { get; set; }
		public List<CreatePetServiceDetailCommand> PetServiceDetails { get; set; }
	}

	public class CreatePetServiceDetailCommand
	{
		public float PetWeightMin { get; set; }
		public float PetWeightMax { get; set; }
		public float Amount { get; set; }
		public bool PetType { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
	}


}
