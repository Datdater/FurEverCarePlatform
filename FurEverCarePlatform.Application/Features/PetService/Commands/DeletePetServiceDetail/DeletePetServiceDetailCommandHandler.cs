using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.DeletePetServiceDetail
{
    class DeletePetServiceDetailCommandHandler : IRequestHandler<DeletePetServiceDetailCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;

		public DeletePetServiceDetailCommandHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<Guid> Handle(DeletePetServiceDetailCommand request, CancellationToken cancellationToken)
		{
			var petService = await _unitOfWork.GetRepository<Domain.Entities.PetServiceDetail>().GetFirstOrDefaultAsync(p => p.Id == request.Id);
			petService.IsDeleted = true;
			_unitOfWork.GetRepository<Domain.Entities.PetServiceDetail>().Update(petService);
			await _unitOfWork.SaveAsync();
			return petService.Id;

		}
	}
}
	