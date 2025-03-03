using System.ComponentModel.DataAnnotations;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductType : BaseEntity
{
    [Required]
	[MaxLength(255)]
	public string Name { get; set; }
    public Guid? ProductId { get; set; }

    //navigation
    public virtual ICollection<ProductTypeDetail> ProductTypeDetails { get; set; }
    public virtual Product Product { get; set; }
}
