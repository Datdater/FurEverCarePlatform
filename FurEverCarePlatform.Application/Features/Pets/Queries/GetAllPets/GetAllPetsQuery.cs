using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.Pets.dto;
using MediatR;

namespace FurEverCarePlatform.Application.Features.Pets.Queries.GetAllPets
{
	public class GetAllPetsQuery : IRequest<Pagination<PetDTO>>
	{
		public string? SearchName { get; set; }
		public Guid? UserId { get; set; }
        public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}
