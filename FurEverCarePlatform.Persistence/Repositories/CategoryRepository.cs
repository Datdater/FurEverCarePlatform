using FurEverCarePlatform.Domain.Entities;
using FurEverCarePlatform.Persistence.DatabaseContext;

namespace FurEverCarePlatform.Persistence.Repositories;

public class CategoryRepository : GenericRepository<ProductCategory>, ICategoryRepository
{
    public CategoryRepository(PetDatabaseContext context) : base(context)
    {
    }
}