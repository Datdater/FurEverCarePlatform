using FurEverCarePlatform.Application.Features.Products.DTOs;

namespace FurEverCarePlatform.Application.MappingProfile;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        //CreateMap<ProductTypeDetailsDTO, ProductTypeDetail>().ReverseMap();
        //CreateMap<ProductType, ProductTypeDTO>().ReverseMap();
        //CreateMap<ProductPrice, ProductPricesDTO>()
        //    .ForMember(dest => dest.ProductTypeDetails1, opt => opt.MapFrom(src => src.ProductType1.Name))
        //    .ForMember(dest => dest.ProductTypeDetails2, opt => opt.MapFrom(src => src.ProductType2.Name))
        //.ReverseMap();
        //CreateMap<List<ProductPrice>, List<ProductPricesDTO>>().ReverseMap();
        //CreateMap<ProductImages, ProductImagesDTO>().ReverseMap();
        CreateMap<Domain.Entities.Product, ProductDTO>()
            .ForMember(
                dest => dest.ProductImage,
                opt => opt.MapFrom(src => src.Images.FirstOrDefault().ImageUrl)
            )
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            .ForMember(
                dest => dest.StoreWard,
                opt => opt.MapFrom(src => src.Store.BusinessAddressWard)
            )
            .ForMember(
                dest => dest.StoreDistrict,
                opt => opt.MapFrom(src => src.Store.BusinessAddressDistrict)
            )
            .ForMember(
                dest => dest.StoreProvince,
                opt => opt.MapFrom(src => src.Store.BusinessAddressProvince)
            )
            .ForMember(
                dest => dest.StoreStreet,
                opt => opt.MapFrom(src => src.Store.BusinessAddressStreet)
            )
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.BasePrice));
        //CreateMap<Domain.Entities.ProductType, ProductTypeDetailsDTO>()
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        //CreateMap<Product, ProductSpecificDTO>()
        //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
        //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
        //    .ForMember(
        //        dest => dest.ProductDescription,
        //        opt => opt.MapFrom(src => src.ProductDescription)
        //    )
        //    .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
        //    .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
        //    .ForMember(
        //        dest => dest.CategoryName,
        //        opt => opt.MapFrom(src => src.ProductCategory.Name)
        //    )
        //    .ForMember(dest => dest.ProductTypes, opt => opt.MapFrom(src => src.ProductTypes));
        CreateMap<Pagination<Domain.Entities.Product>, Pagination<ProductDTO>>()
            .ReverseMap();
    }
}
