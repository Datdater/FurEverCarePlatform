using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService
{
	public class UpdatePetServiceCommandHandler : IRequestHandler<UpdatePetServiceCommand, Guid>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public UpdatePetServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}
		public async Task<Guid> Handle(UpdatePetServiceCommand request, CancellationToken cancellationToken)
		{
			var validator = new UpdatePetServiceValidator();
			var validationResult = await validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException(validationResult.ToString(), validationResult);
			}
			var petService = _mapper.Map<Domain.Entities.PetService>(request);
			_unitOfWork.GetRepository<Domain.Entities.PetService>().Update(petService);
			await _unitOfWork.SaveAsync();
			return petService.Id;
		}
	}
}
