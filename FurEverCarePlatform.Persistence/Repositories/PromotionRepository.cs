
namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PromotionRepository : GenericRepository<Promotion>, IPromotionRepository
	{
		public PromotionRepository(PetDatabaseContext context) : base(context)
		{
		}
    }
}
