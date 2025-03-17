using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking;

namespace FurEverCarePlatform.Application.MappingProfile
{
    public class BookingProfile : Profile
    {
		public BookingProfile()
		{
			CreateMap<CreateBookingCommand, Booking>()
				.ForMember(dest => dest.BookingDetails, opt => opt.Ignore());


			CreateMap<PetCommand, Pet>().ReverseMap();

		}
	}
}
