using AutoMapper;
using FurEverCarePlatform.Application.Features.Pets.dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Pets.Queries.GetAllPets
{
	public class GetAllPetsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllPetsQuery, Pagination<PetDTO>>
	{

		public async Task<Pagination<PetDTO>> Handle(GetAllPetsQuery request, CancellationToken cancellationToken)
		{
			//return mapper.Map<Pagination<PetDTO>>(petRaw);
            throw new NotImplementedException();
		}
	}
}
