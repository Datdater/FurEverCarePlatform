using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class Payment : BaseEntity
{
	public Guid? OrderId { get; set; } 

	public Guid? BookingId { get; set; } 

	[Required]
	[Column(TypeName = "money")]
	public decimal Amount { get; set; } 

	[Required]
	public Guid PaymentMethodId { get; set; } 

	public virtual Order? Order { get; set; }

	public virtual Booking? Booking { get; set; }

	public virtual PaymentMethod PaymentMethod { get; set; }
}
