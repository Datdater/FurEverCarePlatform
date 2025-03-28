using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Product.DTOs;

public class ProductDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? ProductDescription { get; set; }
    public string? BrandName { get; set; }
    public string? StoreName { get; set; }
    public string? CategoryName { get; set; }
    public decimal MinPrices { get; set; }
}
