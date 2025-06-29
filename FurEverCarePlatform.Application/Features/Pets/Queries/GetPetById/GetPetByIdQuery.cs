using FurEverCarePlatform.Application.Features.Pets.dto;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Queries.GetPetById
{
	public class GetPetByIdQuery : IRequest<PetDTO>
	{
		public Guid Id { get; set; }
	}
}
