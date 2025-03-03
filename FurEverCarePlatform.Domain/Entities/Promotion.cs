using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class Promotion : BaseEntity
{
	[Required]
	[MaxLength(255)]
	public string Name { get; set; }
	[Required]
	public Guid StoreId { get; set; }

	[Column(TypeName = "money")]
	public decimal MinPrice { get; set; }

	[Column(TypeName = "money")]
	public decimal MaxPrice { get; set; }

	public float DiscountPercentage { get; set; }

	[Required]
	public DateTime StartDate { get; set; }

	public DateTime? EndDate { get; set; }

	//navigation
	public virtual Store Store { get; set; }

	public virtual ICollection<Booking> Bookings { get; set; }

	public virtual ICollection<Order> Orders { get; set; }
}
