using AutoMapper;
using BuildingBlocks.Core;
using FurEverCarePlatform.Application.Features.Pets.dto;
using MediatR;
using Pet.Application.Feature.Pets.Queries.Spec;
using Pet.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Pets.Queries.GetAllPets
{
	public class GetAllPetsHandler(IPetRepositoy petRepository, IMapper mapper) : IRequestHandler<GetAllPetsQuery, Pagination<PetDTO>>
	{

		public async Task<Pagination<PetDTO>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
		{
			var specification = new PetSpecification(request);
			var petRaw = await petRepository.GetPagedWithSpecAsync(specification);
			return mapper.Map<Pagination<PetDTO>>(petRaw);
		}
	}
}
