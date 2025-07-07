
namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
	public class CreatePetServiceCommand : IRequest<Guid>
	{
		public required string Name { get; set; }
		public string? Description { get; set; }
		public required string EstimatedTime { get; set; }
		public string? Image { get; set; }
		public Guid ServiceCategoryId { get; set; }

        public bool Status { get; set; }
		public List<CreatePetServiceDetailCommand> PetServiceDetails { get; set; }

		public List<CreatePetServiceStepCommand> PetServiceSteps { get; set; }
	}

	public class CreatePetServiceDetailCommand
	{
		public float PetWeightMin { get; set; }
		public float PetWeightMax { get; set; }
		public float Amount { get; set; }
		public bool PetType { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
        public string? Image { get; set; }

    }

    public class CreatePetServiceStepCommand
	{
		public required string Name { get; set; }
		public string? Description { get; set; }
		public int Priority { get; set; }
	}


}
