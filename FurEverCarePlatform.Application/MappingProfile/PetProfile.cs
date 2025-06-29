using FurEverCarePlatform.Application.Features.Pets.Commands.CreatePet;
using FurEverCarePlatform.Application.Features.Pets.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.MappingProfile
{
    public class PetProfile : Profile
    {
        public PetProfile()
        {
            CreateMap<Pet, PetDTO>().ReverseMap();
            CreateMap<CreatePetCommand, Pet>().ReverseMap();


            CreateMap<Pagination<Pet>, Pagination<PetDTO>>().ReverseMap();
        }
    }
}
