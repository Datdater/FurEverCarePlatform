using FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory;
using FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateProductCategoryCommand, ProductCategory>().ReverseMap();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
        CreateMap<Pagination<ProductCategory>, Pagination<ProductCategoryDto>>().ReverseMap();
    }
}
