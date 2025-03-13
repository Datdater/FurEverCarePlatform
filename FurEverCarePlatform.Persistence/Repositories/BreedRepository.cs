
namespace FurEverCarePlatform.Persistence.Repositories
{
    public class BreedRepository : GenericRepository<Breed> , IBreedRepository
	{
		public BreedRepository(PetDatabaseContext context) : base(context)
		{
		}
    }
}
