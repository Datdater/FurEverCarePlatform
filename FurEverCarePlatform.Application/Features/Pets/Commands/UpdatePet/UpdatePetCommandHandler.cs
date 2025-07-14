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

            if (!string.IsNullOrEmpty(request.Name))
                pet.Name = request.Name;

            if (request.Dob != null)
            {
                request.Dob = DateTime.SpecifyKind(request.Dob.Value, DateTimeKind.Utc);
                pet.Dob = request.Dob;
            }
            
            if (request.Image != null)
                pet.Image = request.Image;
            
            if (request.PetType != null)
                pet.PetType = request.PetType.Value;
            if (request.Weight != null)
                pet.Weight = request.Weight.Value;
            
            if (request.Color != null)
                pet.Color = request.Color;
            
            if (request.SpecialRequirement != null)
                pet.SpecialRequirement = request.SpecialRequirement;

            unitOfWork.GetRepository<Pet>().Update(pet);
            await unitOfWork.SaveAsync();
        }
    }
}
