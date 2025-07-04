using AutoMapper;
using FurEverCarePlatform.Application.Features.Products.DTOs;
using FurEverCarePlatform.Domain.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory
{
    public class GetCategoryQueryHandler
        : IRequestHandler<GetProductCategoryQuery, Pagination<ProductCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Pagination<ProductCategoryDto>> Handle(
            GetProductCategoryQuery request,
            CancellationToken cancellationToken
        )
        {
            var categoryDetails = _unitOfWork.GetRepository<Domain.Entities.ProductCategory>()
            .GetQueryable();

            var categoryRaw = await Pagination<Domain.Entities.ProductCategory>.CreateAsync(
                categoryDetails,
                request.PageNumber,
                request.PageSize
            );
            var data = _mapper.Map<Pagination<ProductCategoryDto>>(categoryRaw);
            return data;
        }
    }
}
