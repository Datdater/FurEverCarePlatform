using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService
{
    public class UpdatePetServiceCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid StoreId { get; set; }
        public string? EstimatedTime { get; set; }
        public Guid ServiceCategoryId { get; set; }
        public string? Image {  get; set; }

        public bool Status { get; set; }
        public List<UpdatePetServiceDetailCommand> PetServiceDetails { get; set; }

        public List<UpdatePetServiceStepCommand> PetServiceSteps { get; set; }
    }

    public class UpdatePetServiceDetailCommand
    {
        public Guid Id { get; set; }
        public float? PetWeightMin { get; set; }
        public float? PetWeightMax { get; set; }
        public float? Amount { get; set; }
        public bool PetType { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
    }

    public class UpdatePetServiceStepCommand
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Priority { get; set; }
    }
}
