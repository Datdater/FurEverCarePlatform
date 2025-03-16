
namespace FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory
{
    public class GetProductCategoryQuery : IRequest<Pagination<ProductCategoryDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
