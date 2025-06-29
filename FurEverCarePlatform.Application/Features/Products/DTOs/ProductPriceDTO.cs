using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Features.Products.DTOs;

public class ProductPricesDTO
{
    public decimal Price { get; set; }
    public int Inventory { get; set; }
    public string? URLImage { get; set; }
    public string ProductTypeDetails1 { get; set; }
    public string? ProductTypeDetails2 { get; set; }
}
