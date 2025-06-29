using AutoMapper;
using FurEverCarePlatform.Application.Features.Pets.dto;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Queries.GetPetById
{
	public class GetPetByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetPetByIdQuery, PetDTO>
	{
		public async Task<PetDTO> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
		{
            var pet = await unitOfWork.GetRepository<Pet>().GetByIdAsync(request.Id);
            if (pet == null)
            {
                throw new InvalidOperationException("Pet not found");
            }
            return mapper.Map<PetDTO>(pet);
		}
	}
}
