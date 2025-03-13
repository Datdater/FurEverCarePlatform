
namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PaymentRepository : GenericRepository<Payment>, IPaymentRepository
	{
		public PaymentRepository(PetDatabaseContext context) : base(context)
		{
		}
    }
}
