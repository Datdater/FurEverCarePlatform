using FurEverCarePlatform.Application.Features.Product.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.MappingProfile;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<ProductTypeDetailsDTO, ProductTypeDetail>().ReverseMap();
        CreateMap<ProductType, ProductTypeDTO>().ReverseMap();
        //CreateMap<ProductPrice, ProductPricesDTO>()
        //    .ForMember(dest => dest.ProductTypeDetails1, opt => opt.MapFrom(src => src.ProductType1.Name))
        //    .ForMember(dest => dest.ProductTypeDetails2, opt => opt.MapFrom(src => src.ProductType2.Name))
        //.ReverseMap();
        //CreateMap<List<ProductPrice>, List<ProductPricesDTO>>().ReverseMap();

        CreateMap<Product, ProductDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name));
        CreateMap<Domain.Entities.ProductType, ProductTypeDetailsDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<Product, ProductSpecificDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.ProductCode, opt => opt.MapFrom(src => src.ProductCode))
            .ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.ProductBrand.Name))
            .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ProductCategory.Name))
            .ForMember(dest => dest.ProductTypes, opt => opt.MapFrom(src => src.ProductTypes));
        CreateMap<Pagination<Product>, Pagination<ProductDTO>>().ReverseMap();
    }
}
