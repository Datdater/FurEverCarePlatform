using FurEverCarePlatform.Application.Commons;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory
{
    public class GetProductCategoryQuery : IRequest<Pagination<ProductCategoryDto>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
