using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetServiceStep
{
    public class DeletePetServiceStepCommandHandler : IRequestHandler<DeletePetServiceStepCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeletePetServiceStepCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Guid> Handle(DeletePetServiceStepCommand request, CancellationToken cancellationToken)
		{
			var petServiceStep = await _unitOfWork.GetRepository<Domain.Entities.PetServiceStep>().GetFirstOrDefaultAsync(p => p.Id == request.Id);
			petServiceStep.IsDeleted = true;
			_unitOfWork.GetRepository<Domain.Entities.PetServiceStep>().Update(petServiceStep);
			await _unitOfWork.SaveAsync();
			return petServiceStep.Id;

		}
	}
}
