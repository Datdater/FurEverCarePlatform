namespace FurEverCarePlatform.Persistence.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(PetDatabaseContext context)
        : base(context) { }

   
}
