using FurEverCarePlatform.Application.Contracts;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Store.Queries.GetAllProductAndService
{
    public class GetAllServiceByStoreHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllServiceByStoreQuery, Pagination<PetServicesDto>>
    {
        public async Task<Pagination<PetServicesDto>> Handle(GetAllServiceByStoreQuery request, CancellationToken cancellationToken)
        {
            var petService = unitOfWork
                .GetRepository<Domain.Entities.PetService>()
                .GetQueryable()
                .Include(ps => ps.Store)
                .Include(ps => ps.ServiceCategory)
                .Include(ps => ps.PetServiceDetails)
                .Include(ps => ps.PetServiceSteps)
                .Where(s => !s.IsDeleted);
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                petService = petService.Where(x => x.Name.ToLower().Contains(request.SearchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(request.SortBy))
            {
                petService = request.SortBy switch
                {
                    "name" => petService.OrderBy(x => x.Name),
                    "price" => petService.OrderBy(x => x.PetServiceDetails.Min(d => d.Amount)),
                    _ => petService.OrderBy(x => x.Name),
                };
            }


            var petServicePagination = await Pagination<Domain.Entities.PetService>.CreateAsync(
                petService,
                request.PageNumber,
                request.PageSize
            );
            var data = mapper.Map<Pagination<PetServicesDto>>(petServicePagination);
            return data;
        }
    }
}
