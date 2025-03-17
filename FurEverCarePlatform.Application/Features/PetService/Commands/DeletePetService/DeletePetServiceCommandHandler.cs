using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetService
{
    public class DeletePetServiceCommandHandler(IUnitOfWork unitOfWork)
    : IRequestHandler<DeletePetServiceCommand>
    {
        public async Task Handle(DeletePetServiceCommand request, CancellationToken cancellationToken)
        {
            var petService = await unitOfWork.PetServiceRepository
                .GetByIdAsync(request.Id);

            if (petService == null)
            {
                throw new NotFoundException(nameof(Domain.Entities.PetService), request.Id);
            }

            
            unitOfWork.PetServiceRepository.Update(petService);

            await unitOfWork.SaveAsync();

        }

        
    }
}
