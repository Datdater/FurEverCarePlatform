using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.UpdatePet
{
    public class UpdatePetCommandHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdatePetCommand>
    {
        public async Task Handle(UpdatePetCommand request, CancellationToken cancellationToken)
        {
            var pet = await unitOfWork.GetRepository<Pet>().GetByIdAsync(request.Id);
            if (pet == null)
            {
                throw new InvalidOperationException("Pet not found");
            }
            pet.Name = request.Name;
            pet.Dob = request.Dob;
            pet.Image = request.Image;
            pet.PetType = request.PetType;
            pet.Color = request.Color;
            pet.SpecialRequirement = request.SpecialRequirement;
            unitOfWork.GetRepository<Pet>().Update(pet);
            await unitOfWork.SaveAsync();
        }
    }
}
