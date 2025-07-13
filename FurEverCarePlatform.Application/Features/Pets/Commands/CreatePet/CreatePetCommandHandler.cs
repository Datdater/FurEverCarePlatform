using AutoMapper;
using FurEverCarePlatform.Application.Commons.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.CreatePet
{
    public class CreatePetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IClaimService claimService)
        : IRequestHandler<CreatePetCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreatePetCommand request,
            CancellationToken cancellationToken
        )
        {
            var userId = claimService.GetCurrentUser;
            request.Dob = DateTime.SpecifyKind(request.Dob, DateTimeKind.Utc);
            var pet = mapper.Map<Pet>(request);
            pet.AppUserId = userId;
            await unitOfWork.GetRepository<Pet>().InsertAsync(pet);
            await unitOfWork.SaveAsync();
            return pet.Id;
        }
    }
}
