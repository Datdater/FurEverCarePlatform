namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PaymentRepository : GenericRepository<Domain.Entities.Payment>, IPaymentRepository
    {
        public PaymentRepository(PetDatabaseContext context)
            : base(context) { }
    }
}
