using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;


public class Order : BaseEntity
{
	public required Guid UserId { get; set; } 

	[Column(TypeName = "money")]
	public required decimal TotalPrice { get; set; } 

	public Guid? PromotionId { get; set; } 

	public required Guid AddressId { get; set; }

	public float Distance { get; set; } 

	public virtual AppUser AppUser { get; set; }

	public virtual Promotion Promotion { get; set; }

	public virtual Address Address { get; set; }

	public virtual ICollection<OrderDetail> OrderDetails { get; set; }

	public virtual ICollection<OrderStatus> OrderStatus { get; set; }

	public virtual Payment Payment { get; set; }

}
