using System.ComponentModel.DataAnnotations.Schema;
using FurEverCarePlatform.Domain.Enums;

namespace FurEverCarePlatform.Domain.Entities;

public class Order : BaseEntity
{
    public required Guid AppUserId { get; set; }

    [Column(TypeName = "money")]
    public required float TotalPrice { get; set; }
    public required string Code { get; set; }

    public Guid? PromotionId { get; set; }

    public required Guid AddressId { get; set; }

    public EnumOrderStatus OrderStatus { get; set; }
    public DateTime OrderDate { get; private set; } = DateTime.UtcNow.AddHours(7);
    public DateTime? OrderCompletedAt { get; set; }
    public string Note { get; set; }
    public decimal DeliveryPrice { get; set; }
    public virtual AppUser AppUser { get; set; }

    public virtual Promotion Promotion { get; set; }

    public virtual Address Address { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; }

    public virtual Payment Payment { get; set; }
}
