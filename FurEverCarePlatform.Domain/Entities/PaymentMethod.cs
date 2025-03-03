using System.ComponentModel.DataAnnotations;

namespace FurEverCarePlatform.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    [Required]
	[StringLength(255)]
	public string Name { get; set; }

    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
