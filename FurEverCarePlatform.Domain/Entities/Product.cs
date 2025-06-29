using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace FurEverCarePlatform.Domain.Entities;
public class Product : BaseEntity
{
    public Guid CategoryId { get; set; }
    public Guid StoreId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public float BasePrice { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Height { get; set; }
    public int Sold { get; set; }
    public double StarAverage { get; set; } = 5.0;
    public int ReviewCount { get; set; } = 0;
    public int TotalRating { get; set; } = 0;

    // Navigation properties
    public virtual Store Store { get; set; }
    [ForeignKey("CategoryId")]
    public virtual ProductCategory Category { get; set; }
    public virtual List<ProductVariant> Variants { get; set; }
    public List<ProductImage> Images { get; set; }
    public virtual List<ProductReviews> ProductReviews { get; set; }

    public Product() : base()
    {
        ProductReviews = new List<ProductReviews>();
        Variants = new List<ProductVariant>();
        Images = new List<ProductImage>();
    }

    public void AddReview(ProductReviews review)
    {
        ProductReviews.Add(review);
        TotalRating += review.Rating;
        ReviewCount = ProductReviews.Count;
        StarAverage = Math.Round((double)TotalRating / ReviewCount, 1);
    }

    public void RemoveReview(ProductReviews review)
    {
        if (ProductReviews.Remove(review))
        {
            TotalRating -= review.Rating;
            ReviewCount = ProductReviews.Count;
            StarAverage = ReviewCount > 0 ? Math.Round((double)TotalRating / ReviewCount, 1) : 0;
        }
    }

    public void UpdateSold(int quantity)
    {
        Sold += quantity;
    }
}

public class ProductVariant : BaseEntity
{
    public Guid ProductId { get; set; }
    public JsonDocument Attributes { get; set; }
    public float Price { get; set; }
    public int Stock { get; set; }

    // Navigation property nếu dùng EF Core
    public virtual Product Product { get; set; }
    public ProductVariant() : base() { }
}
public class ProductImage : BaseEntity
{
    public Guid ProductId { get; set; }
    public bool IsMain { get; set; }
    public string ImageUrl { get; set; }
    public Product Product { get; set; }
    public ProductImage() : base() { }
}