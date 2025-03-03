using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductBrand : BaseEntity
{
	[MaxLength(255)]
	public required string Name { get; set; }
	[MaxLength(2048)]
	public string? BrandLink { get; set; }
	public virtual ICollection<Product> Products { get; set; }
}