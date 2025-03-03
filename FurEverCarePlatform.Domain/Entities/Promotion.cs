using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class Promotion : BaseEntity
{
	[StringLength(255)]
	public required string Name { get; set; }

	public required Guid StoreId { get; set; }

	[Column(TypeName = "money")]
	public decimal MinPrice { get; set; }

	[Column(TypeName = "money")]
	public decimal MaxPrice { get; set; }

	public float DiscountPercentage { get; set; }

	public required DateTime StartDate { get; set; }

	public DateTime? EndDate { get; set; }

	//navigation
	public virtual Store Store { get; set; }

	public virtual ICollection<Booking> Bookings { get; set; }

	public virtual ICollection<Order> Orders { get; set; }
}
