using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.UpdatePet
{
    public class UpdatePetCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Dob { get; set; }
        public string Image { get; set; }
        public float Weight { get; set; }
        public bool PetType { get; set; }
        public string? Color { get; set; }
        public string? SpecialRequirement { get; set; }
    }
}
