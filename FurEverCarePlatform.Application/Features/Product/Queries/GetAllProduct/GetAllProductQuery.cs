using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.Queries.GetAllProduct;

public class GetAllProductQuery : IRequest<Pagination<ProductDTO>>
{
    public int PageNumber { get; set; } = 0;
    public int PageSize { get; set; } = 5;
}
