using FurEverCarePlatform.Application.Features.Products.DTOs;

namespace FurEverCarePlatform.Application.Features.Products.Queries.GetAllProduct;

public class GetAllProductQuery : IRequest<Pagination<ProductDTO>>
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
