using AutoMapper;
using FurEverCarePlatform.Application.Features.Pets.dto;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Queries.GetPetById
{
	public class GetPetByIdHandler(IPetRepositoy petRepository, IMapper mapper) : IRequestHandler<GetPetByIdQuery, PetDTO>
	{
		public async Task<PetDTO> Handle(GetPetByIdQuery request, CancellationToken cancellationToken)
		{
			var pet = await petRepository.SingleOrDefaultAsync(p => p.Id == request.Id);
			if (pet == null)
			{
				throw new NotFoundException(nameof(Pet), request.Id);
			}
			return mapper.Map<PetDTO>(pet);
		}
	}
}
