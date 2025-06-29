using FurEverCarePlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Commands.UpdateBooking
{
	public class UpdateBookingCommand : IRequest
	{
		public Guid Id { get; set; }

		public BookingStatus BookingStatus { get; set; }
	}
}
