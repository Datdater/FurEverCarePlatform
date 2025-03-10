using AutoMapper;
using FurEverCarePlatform.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory
{
    public class GetCategoryQueryHandler : IRequestHandler<GetProductCategoryQuery, Pagination<ProductCategoryDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetCategoryQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<Pagination<ProductCategoryDto>> Handle(GetProductCategoryQuery request, CancellationToken cancellationToken)
        {
            var categoryDetails = await _unitOfWork.CategoryRepository.GetPaginationAsync(null, request.PageNumber, request.PageSize);
            var data = _mapper.Map<Pagination<ProductCategoryDto>>(categoryDetails);
            return data;
        }
    }
}
