using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;
using FurEverCarePlatform.Application.Features.PetService.DTOs;

namespace FurEverCarePlatform.Application.MappingProfile
{
    public class PetServiceProfile : Profile
    {
		public PetServiceProfile()
		{
            //CreateMap<CreatePetServiceCommand, PetService>()
            //	.ForMember(dest => dest.PetServiceDetails, opt => opt.MapFrom(src => src.PetServiceDetails));

            //CreateMap<CreatePetServiceDetailCommand, PetServiceDetail>();
            ////CreateMap<PetService, PetServiceDto>().ReverseMap();
            ///// Ánh xạ từ CreatePetServiceCommand sang PetService
            // Ánh xạ từ CreatePetServiceCommand sang PetService (đã có từ trước)
            CreateMap<CreatePetServiceCommand, PetService>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PetServiceDetails, opt => opt.MapFrom(src => src.PetServiceDetails));

            CreateMap<CreatePetServiceDetailCommand, PetServiceDetail>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PetServiceId, opt => opt.Ignore());

            // Ánh xạ từ PetService sang PetServiceDto
            CreateMap<PetService, PetServiceDto>()
                .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store != null ? src.Store.Name : string.Empty))
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory != null ? src.ServiceCategory.Name : string.Empty))
                .ForMember(dest => dest.PetServiceSteps, opt => opt.MapFrom(src => src.PetServiceSteps ?? new List<PetServiceStep>()))
                .ForMember(dest => dest.PetServiceDetails, opt => opt.MapFrom(src => src.PetServiceDetails ?? new List<PetServiceDetail>()))
                .ForMember(dest => dest.ComboServices, opt => opt.MapFrom(src => src.ComboServices ?? new List<ComboService>()));

            // Ánh xạ từ PetServiceDetail sang PetServiceDetailDto
            CreateMap<PetServiceDetail, PetServiceDetailDto>();

            // Ánh xạ từ PetServiceStep sang PetServiceStepDto
            CreateMap<PetServiceStep, PetServiceStepDto>();

            // Ánh xạ từ ComboService sang ComboServiceDto
            CreateMap<ComboService, ComboServiceDto>();
        }
	}
}
