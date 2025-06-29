using AutoMapper;
using FurEverCarePlatform.Application.Features.Pets.dto;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices;
using FurEverCarePlatform.Domain.Entities;
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
            var pets = unitOfWork.GetRepository<Pet>().GetQueryable();

            var petPagination = await Pagination<Pet>.CreateAsync(
                pets,
                request.PageIndex,
                request.PageSize
            );
            var data = mapper.Map<Pagination<PetDTO>>(petPagination);
            return data;
        }
	}
}
