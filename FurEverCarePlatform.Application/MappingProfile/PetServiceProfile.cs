using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.PetService.Commands.CreatePetService;

namespace FurEverCarePlatform.Application.MappingProfile
{
    public class PetServiceProfile : Profile
    {
		public PetServiceProfile()
		{
			CreateMap<CreatePetServiceCommand, PetService>()
				.ForMember(dest => dest.PetServiceDetails, opt => opt.MapFrom(src => src.PetServiceDetails));

			CreateMap<CreatePetServiceDetailCommand, PetServiceDetail>();
			//CreateMap<PetService, PetServiceDto>().ReverseMap();
			//CreateMap<Pagination<PetService>, Pagination<PetServiceDto>>().ReverseMap();
		}
	}
}
