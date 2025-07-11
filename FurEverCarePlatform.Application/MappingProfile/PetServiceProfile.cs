using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using FurEverCarePlatform.Application.Features.PetService.Commands.UpdatePetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetService;
using FurEverCarePlatform.Application.Features.PetService.Queries.GetPetServices;

namespace FurEverCarePlatform.Application.MappingProfile
{
    public class PetServiceProfile : Profile
    {
        public PetServiceProfile()
        {
            #region CreatePetService
            CreateMap<CreatePetServiceCommand, Domain.Entities.PetService>()
                .ForMember(
                    dest => dest.PetServiceDetails,
                    opt => opt.MapFrom(src => src.PetServiceDetails)
                )
                .ForMember(
                    dest => dest.PetServiceSteps,
                    opt => opt.MapFrom(src => src.PetServiceSteps)
                );

            CreateMap<CreatePetServiceDetailCommand, PetServiceDetail>();
            CreateMap<CreatePetServiceStepCommand, PetServiceStep>();
            #endregion

            #region PetService
            CreateMap<Domain.Entities.PetService, PetServiceDto>()
                .ForMember(
                    dest => dest.PetServiceDetails,
                    opt => opt.MapFrom(src => src.PetServiceDetails)
                )
                .ForMember(
                    dest => dest.PetServiceSteps,
                    opt => opt.MapFrom(src => src.PetServiceSteps)
                );
            CreateMap<PetServiceDetail, PetServiceDetailDto>().ReverseMap();
            CreateMap<PetServiceStep, PetServiceStepDto>().ReverseMap();
            #endregion


            #region UpdatePetService
            CreateMap<UpdatePetServiceCommand, Domain.Entities.PetService>()
                .ForMember(
                    dest => dest.PetServiceDetails,
                    opt => opt.MapFrom(src => src.PetServiceDetails)
                )
                .ForMember(
                    dest => dest.PetServiceSteps,
                    opt => opt.MapFrom(src => src.PetServiceSteps)
                );
            CreateMap<UpdatePetServiceDetailCommand, PetServiceDetail>();
            CreateMap<UpdatePetServiceStepCommand, PetServiceStep>();
            #endregion

            CreateMap<Domain.Entities.PetService, PetServicesDto>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.ServiceCategory.Name)
                )
                .ForMember(
                    dest => dest.BasePrice,
                    opt => opt.MapFrom(src => src.PetServiceDetails.Min(x => x.Amount))
                )
                .ForMember(
                    dest => dest.TotalUsed,
                    opt => opt.MapFrom(src => src.TotalUsed)
                )
                .ForMember(
                    dest => dest.TotalReviews,
                    opt => opt.MapFrom(src => src.TotalReviews)
                )
                .ForMember(
                    dest => dest.RatingAverage,
                    opt => opt.MapFrom(src => src.RatingAverage)
                )
                .ForMember(
                    dest => dest.BasePrice,
                    opt => opt.MapFrom(src => src.PetServiceDetails.Min(x => x.Amount))
                )
                .ForMember(
                    dest => dest.CategoryName,
                    opt => opt.MapFrom(src => src.ServiceCategory.Name)
                )
                .ForMember(
                    dest => dest.StoreCity,
                    memberOptions => memberOptions.MapFrom(src => src.Store.BusinessAddressProvince)
                )
                .ForMember(
                    dest => dest.StoreDistrict,
                    memberOptions => memberOptions.MapFrom(src => src.Store.BusinessAddressDistrict)
                )
                .ReverseMap();

            CreateMap<Pagination<Domain.Entities.PetService>, Pagination<PetServicesDto>>()
                .ReverseMap();
        }
    }
}
