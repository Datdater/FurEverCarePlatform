namespace FurEverCarePlatform.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PetDatabaseContext context)
        : base(context) { }

    public decimal GetMinProductPrice(Guid productId)
    {
        var query =
            from p in _context.Products
            join pt in _context.ProductTypes on p.Id equals pt.ProductId into ptGroup
            from pt in ptGroup.DefaultIfEmpty() // RIGHT JOIN

            join ptd in _context.ProductTypeDetails on pt.Id equals ptd.ProductTypeId into ptdGroup
            from ptd in ptdGroup.DefaultIfEmpty() // LEFT JOIN

            join pp in _context.ProductPrices on ptd.Id equals pp.ProductTypeDetails1 into ppGroup
            from pp in ppGroup.DefaultIfEmpty() // LEFT JOIN

            where p.Id == productId && pp.Price != null

            select pp.Price;

        return query.Min();
    }

    public List<ProductPrice> GetProductPrices(Guid productId)
    {
        var query =
            from p in _context.Products
            join pt in _context.ProductTypes on p.Id equals pt.ProductId into ptGroup
            from pt in ptGroup.DefaultIfEmpty() // RIGHT JOIN

            join ptd in _context.ProductTypeDetails on pt.Id equals ptd.ProductTypeId into ptdGroup
            from ptd in ptdGroup.DefaultIfEmpty() // LEFT JOIN

            join pp in _context.ProductPrices on ptd.Id equals pp.ProductTypeDetails1 into ppGroup
            from pp in ppGroup.DefaultIfEmpty() // LEFT JOIN

            where p.Id == productId && pp.Price != null

            select pp;
        return query.ToList();
    }

    public List<ProductImages> GetProductImages(Guid productId)
    {
        var query =
            from p in _context.Products
            join pi in _context.ProductImages on p.Id equals pi.ProductId into piGroup
            from pi in piGroup.DefaultIfEmpty() // RIGHT JOIN
            where p.Id == productId
            select pi;
        return query.ToList();
    }
}
