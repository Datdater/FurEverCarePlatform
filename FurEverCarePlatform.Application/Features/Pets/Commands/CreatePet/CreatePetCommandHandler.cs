using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Commands.CreatePet
{
    public class CreatePetCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        : IRequestHandler<CreatePetCommand, Guid>
    {
        public async Task<Guid> Handle(
            CreatePetCommand request,
            CancellationToken cancellationToken
        )
        {
            var pet = mapper.Map<Pet>(request);
            await unitOfWork.GetRepository<Pet>().InsertAsync(pet);
            return pet.Id;
        }
    }
}
