using System.ComponentModel.DataAnnotations;
using FurEverCarePlatform.Domain.Enums;
namespace FurEverCarePlatform.Domain.Entities;

public class OrderStatus : BaseEntity
{
	[StringLength(255)]
	public string? Description { get; set; }

	public EnumOrderStatus Status { get; set; }

	public Guid OrderId { get; set; }

	public virtual Order Order { get; set; }
}
