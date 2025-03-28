using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FurEverCarePlatform.Domain.Entities;

public class Product : BaseEntity
{
	[Required]
	public Guid ProductCategoryId { get; set; }

	[Required]
	[MaxLength(100)]
	public string Name { get; set; } = string.Empty;

	public bool? IsActive { get; set; }

	[MaxLength(50)]
	public string? ProductDescription { get; set; }
    public decimal Weight { get; set; }
    public int Length { get; set; } 
    public int Height { get; set; } 
    public int Width { get; set; } 

	public int? Views { get; set; }

	[Required]
	public Guid BrandId { get; set; }

	[Required]
	public Guid StoreId { get; set; }

	// Navigation properties
	public virtual ProductCategory ProductCategory { get; set; }
	public virtual ProductBrand ProductBrand { get; set; }
	public virtual Store Store { get; set; }

    public virtual ICollection<ProductImages> ProductImages { get; set; } = new List<ProductImages>();
    public virtual ICollection<ProductType> ProductTypes { get; set; } = new List<ProductType>();
	public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
