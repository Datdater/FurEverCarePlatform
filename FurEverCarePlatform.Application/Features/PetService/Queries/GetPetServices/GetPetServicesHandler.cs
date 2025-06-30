using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using FurEverCarePlatform.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices
{
    public class GetPetServicesHandler
        : IRequestHandler<GetPetServicesQuery, Pagination<PetServicesDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetPetServicesHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<PetServicesDto>> Handle(
            GetPetServicesQuery request,
            CancellationToken cancellationToken
        )
        {
            var petService = _unitOfWork
                .GetRepository<Domain.Entities.PetService>()
                .GetQueryable()
                .Include(ps => ps.Store)
                .Include(ps => ps.ServiceCategory)
                .Where(s => !s.IsDeleted);

            var petServicePagination = await Pagination<Domain.Entities.PetService>.CreateAsync(
                petService,
                request.PageIndex,
                request.PageSize
            );
            var data = _mapper.Map<Pagination<PetServicesDto>>(petServicePagination);
            return data;
        }
    }
}
