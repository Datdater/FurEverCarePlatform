using AutoMapper;
using FurEverCarePlatform.Application.Commons;
using FurEverCarePlatform.Application.Features.ProductCategory.Commands.CreateProductCategory;
using FurEverCarePlatform.Application.Features.ProductCategory.Queries.GetAllProductCategory;
using FurEverCarePlatform.Domain.Entities;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<CreateProductCategoryCommand, ProductCategory>().ReverseMap();
        CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
        CreateMap<Pagination<ProductCategory>, Pagination<ProductCategoryDto>>().ReverseMap();
    }
}
