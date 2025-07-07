using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Products.DTOs;

public class ProductDTO
{
    public string Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string StoreId { get; set; }
    public string StoreName { get; set; }
    public string StoreLogoUrl { get; set; }
    public string StoreProvince { get; set; }
    public string StoreDistrict { get; set; }
    public string StoreWard { get; set; }
    public string StoreStreet { get; set; }
    public string CategoryId { get; set; }

    public string CategoryName { get; set; }
    public string ProductImage { get; set; }
    public double StarAverage { get; set; }
    public int ReviewCount { get; set; }
    public int Sold { get; set; }
}
