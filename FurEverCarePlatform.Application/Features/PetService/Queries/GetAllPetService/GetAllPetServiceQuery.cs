using FurEverCarePlatform.Application.Features.PetService.DTOs;
using FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetAllPetService
{
    public class GetAllPetServiceQuery : IRequest<Pagination<PetServiceDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
