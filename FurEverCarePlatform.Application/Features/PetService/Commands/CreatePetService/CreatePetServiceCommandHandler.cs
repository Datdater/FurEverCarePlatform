using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory;
using FurEverCarePlatform.Domain.Entities;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
    public class CreatePetServiceCommandHandler : IRequestHandler<CreatePetServiceCommand, Guid>
	{
        private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public CreatePetServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}

		public async Task<Guid> Handle(CreatePetServiceCommand request, CancellationToken cancellationToken)
		{
			var validator = new CreatePetServiceValidator();
			var validationResult = await validator.ValidateAsync(request);
			if (!validationResult.IsValid)
			{
				throw new BadRequestException(validationResult.ToString(), validationResult);
			}
			var petService = _mapper.Map<Domain.Entities.PetService>(request);
			await _unitOfWork.GetRepository<Domain.Entities.PetService>().InsertAsync(petService);
			await _unitOfWork.SaveAsync();
			return petService.Id;
		}
	}
}
