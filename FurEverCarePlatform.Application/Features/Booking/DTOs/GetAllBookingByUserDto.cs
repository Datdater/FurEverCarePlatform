using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.DTOs
{
	public class GetAllBookingByUserDto
	{
		public DateTime BookingTime { get; set; }
		public string? Description { get; set; }
		public Guid? PromotionId { get; set; }

		public string DeliveryAddress { get; set; }
		public string DeliveryCity { get; set; }
		public string DeliveryContactPhone { get; set; }
	}

	public class PetInformation
	{
		public string Name { get; set; }

		public string PetType { get; set; }

		public Guid PetId { get; set; }

	}

}
