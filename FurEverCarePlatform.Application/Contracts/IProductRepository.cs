using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Application.Contracts;

public interface IProductRepository : IGenericRepository<Product>
{
    public decimal GetMinProductPrice(Guid productId);
    public List<ProductPrice> GetProductPrices(Guid productId);
}
