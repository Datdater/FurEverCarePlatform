using FurEverCarePlatform.Application.Features.PetService.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService
{
    public class GetPetServiceQuery : IRequest<PetServiceDto>
    {
        public GetPetServiceQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
