using FurEverCarePlatform.Application.Contracts;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
    public class CreatePetServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreatePetServiceCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<Guid> Handle(
            CreatePetServiceCommand request,
            CancellationToken cancellationToken
        )
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
