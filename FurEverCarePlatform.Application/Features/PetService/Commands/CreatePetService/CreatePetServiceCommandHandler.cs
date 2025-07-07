using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;
using FurEverCarePlatform.Application.Contracts;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService
{
    public class CreatePetServiceCommandHandler : IRequestHandler<CreatePetServiceCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IClaimService _claimService;

        public CreatePetServiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _claimService = claimService;
        }

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
            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var userId = _claimService.GetCurrentUser;

                var store = await _unitOfWork.GetRepository<Domain.Entities.Store>().GetQueryable().FirstOrDefaultAsync(x => x.AppUserId == userId);
                if (store == null) throw new System.Exception("Not found store with this user");
                var petService = _mapper.Map<Domain.Entities.PetService>(request);
                petService.StoreId = store.Id;
                await _unitOfWork
                    .GetRepository<Domain.Entities.PetService>()
                    .InsertAsync(petService);
                await _unitOfWork.SaveAsync();
                await _unitOfWork.CommitTransactionAsync();
                return petService.Id;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}
