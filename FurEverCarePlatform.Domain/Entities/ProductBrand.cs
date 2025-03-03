using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductBrand : BaseEntity
{
	[StringLength(255)]
	public required string Name { get; set; }
	public string? BrandLink { get; set; }
	public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}