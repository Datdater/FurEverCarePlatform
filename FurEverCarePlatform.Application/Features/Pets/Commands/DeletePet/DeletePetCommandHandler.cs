using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.DeletePet
{
	public class DeletePetCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePetCommand>
	{
		public async Task Handle(DeletePetCommand request, CancellationToken cancellationToken)
		{
			var pet = await unitOfWork.GetRepository<Pet>().GetByIdAsync(request.Id);
			if (pet == null)
			{
				throw new InvalidOperationException("Pet not found");
			}
		     unitOfWork.GetRepository<Pet>().Delete(pet);
            await unitOfWork.SaveAsync();
		}
	}
}
