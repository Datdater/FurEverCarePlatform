using FurEverCarePlatform.Application.Features.Product.DTOs;

namespace FurEverCarePlatform.Application.Features.Product.Queries.GetAllProduct;

public class GetAllProductQuery : IRequest<Pagination<ProductDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
