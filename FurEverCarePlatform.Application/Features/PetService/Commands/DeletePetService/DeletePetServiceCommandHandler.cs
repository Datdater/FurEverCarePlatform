using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetService
{
    public class DeletePetServiceCommandHandler : IRequestHandler<DeletePetServiceCommand, Guid>
    {
		private readonly IUnitOfWork _unitOfWork;

		public DeletePetServiceCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
	    public async Task<Guid> Handle(DeletePetServiceCommand request, CancellationToken cancellationToken)
	    {
			var petService = await _unitOfWork.GetRepository<Domain.Entities.PetService>().GetByIdAsync(request.Id);
			petService.IsDeleted = true;
			_unitOfWork.GetRepository<Domain.Entities.PetService>().Update(petService);
			await _unitOfWork.SaveAsync();
			return petService.Id;
		}
    }
}
