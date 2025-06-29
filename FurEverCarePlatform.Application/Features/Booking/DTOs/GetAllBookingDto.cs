using FurEverCarePlatform.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Booking.DTOs
{
	public class GetAllBookingDto
	{
		public Guid BookingId { get; set; }
		public string ShopName { get; set; } = string.Empty;
		public string UserName { get; set; } = string.Empty;
		public string UserPhone { get; set; } = string.Empty;
		public BookingStatus Status { get; set; }
		public float TotalPrice { get; set; }
		public DateTime BookingTime { get; set; }
		public ICollection<PetWithServices> PetWithServices { get; set; } = new List<PetWithServices>();
	}

	public class PetWithServices
	{
		public Pet Pet { get; set; } = new Pet();
		public ICollection<Service> Services { get; set; } = new List<Service>();
	}

	public class Pet
	{
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public DateTime? Dob { get; set; }
		public bool PetType { get; set; }
		public string? Color { get; set; }
	}

	public class Service
	{
		public Guid Id { get; set; }
		public string ServiceDetailName { get; set; } = string.Empty;
		public float Price { get; set; }
	}
}