
namespace FurEverCarePlatform.Persistence.Repositories
{
    public class BookingRepository : GenericRepository<Booking>, IBookingRepository
	{
		public BookingRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
