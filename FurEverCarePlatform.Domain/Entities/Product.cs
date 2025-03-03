using System.ComponentModel.DataAnnotations;
namespace FurEverCarePlatform.Domain.Entities;

public class Product : BaseEntity
{
    public required Guid ProductCategoryId { get; set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    public bool? IsActive { get; set; }
    public string? ProductCode { get; set; }
    public int? Views { get; set; }
    public required Guid BrandId { get; set; }
    public required Guid StoreId { get; set; }

    //navigation
    public virtual ProductCategory ProductCategory { get; set; }
    public virtual ProductBrand ProductBrand { get; set; }
    public virtual Store Store { get; set; }
    public ICollection<ProductType> ProductTypes { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; }
}
