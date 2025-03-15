using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.Commands.CreateBooking
{
     public class CreateBookingCommand : IRequest<Guid>
	{
		public required DateTime BookingTime { get; set; }
		public string? Description { get; set; }

		public required Guid UserId { get; set; }
		public required decimal RawAmount { get; set; }
		public required decimal TotalAmount { get; set; }
		public float Distance { get; set; }
		public required ICollection<BookingDetailCommand> BookingDetails { get; set; }

	}

	public class BookingDetailCommand
	{
		public required PetCommand Pet { get; set; }
		public required ICollection<ServiceDetailCommand> Services { get; set; }

		public ComboCommand? Combo { get; set; }
	}


	public class PetCommand
	{
		public Guid? Id { get; set; }
		public required string Name { get; set; }
		public bool? PetType { get; set; }
		public DateTime? Age { get; set; }

		public string? SpecialRequirement { get; set; }
		public required Guid UserId { get; set; }
		public string? Color { get; set; }
	}

	public class ServiceDetailCommand
	{
		public Guid Id { get; set; }

		public decimal Amount { get; set; }
	}

	public class ComboCommand
	{
		public Guid? Id { get; set; }
	}
}
