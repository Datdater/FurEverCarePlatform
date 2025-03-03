using System.ComponentModel.DataAnnotations;
namespace FurEverCarePlatform.Domain.Entities;

public class ProductCategory : BaseEntity
{
    [MaxLength(100)]
    public required string Name { get; set; }
	public Guid? ParentCategoryId { get; set; }

	//navigation	
	public virtual ProductCategory? ParentCategory { get; set; }
	public virtual ICollection<ProductCategory> SubCategories { get; set; } = new List<ProductCategory>();
	public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
 