using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;

public class Combo : BaseEntity
{
	[Required]
	[StringLength(255)]
	public string Name { get; set; }

	[Required]
	public float DiscountPercent { get; set; }

	public virtual ICollection<ComboService> ComboServices { get; set; }
	public virtual ICollection<BookingDetail> BookingDetails { get; set; }
}
