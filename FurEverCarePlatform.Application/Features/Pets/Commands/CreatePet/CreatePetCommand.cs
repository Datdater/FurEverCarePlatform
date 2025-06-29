using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.CreatePet
{
    public class CreatePetCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Image { get; set; }
        public float Weight { get; set; }
        public bool PetType { get; set; }
        public Guid AppUserId { get; set; }
        public string? Color { get; set; }
        public string? SpecialRequirement { get; set; }
    }
}
