using FurEverCarePlatform.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;


public class Order : BaseEntity
{
	public required Guid UserId { get; set; } 

	[Column(TypeName = "money")]
	public required decimal TotalPrice { get; set; } 

	public Guid? PromotionId { get; set; } 

	public required Guid AddressId { get; set; }

    public EnumOrderStatus OrderStatus { get; private set; }
    public DateTime OrderDate { get; private set; }
    public string Note { get; private set; }
    public decimal DeliveryPrice { get; private set; }
    public virtual AppUser AppUser { get; set; }

	public virtual Promotion Promotion { get; set; }

	public virtual Address Address { get; set; }

	public virtual ICollection<OrderDetail> OrderDetails { get; set; }

	public virtual Payment Payment { get; set; }

}
