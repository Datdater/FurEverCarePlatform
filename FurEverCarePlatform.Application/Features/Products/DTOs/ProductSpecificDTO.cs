using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Products.DTOs;

public class ProductSpecificDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid StoreId { get; set; }
    public string StoreName { get; set; }
    public string StoreUrl { get; set; }
    public string CategoryName { get; set; }
    public decimal BasePrice { get; set; }
    public decimal Weight { get; set; }
    public decimal Length { get; set; }
    public decimal Height { get; set; }
    public int Sold { get; set; }
    public double StarAverage { get; set; }
    public int ReviewCount { get; set; }
    public List<ProductVariantDTO> Variants { get; set; }
    public List<ProductImageDTO> Images { get; set; }
    public List<ProductReviewDTO> Reviews { get; set; }
}

public class ProductReviewDTO
{
    public Guid Id { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class ProductVariantDTO
{
    [Required]
    public JsonDocument Attributes { get; set; }

    [Required]
    [Range(0.01, float.MaxValue)]
    public float Price { get; set; }

    [Required]
    [Range(0, int.MaxValue)]
    public int Stock { get; set; }
}

public class ProductImageDTO
{
    [Required]
    public string ImageUrl { get; set; }

    public bool IsMain { get; set; }
}
