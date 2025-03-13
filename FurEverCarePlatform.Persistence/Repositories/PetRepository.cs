
namespace FurEverCarePlatform.Persistence.Repositories
{
	public class PetRepository : GenericRepository<Pet>, IPetRepository
	{
		public PetRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
