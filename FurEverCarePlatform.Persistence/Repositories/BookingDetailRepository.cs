using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class BookingDetailRepository : GenericRepository<BookingDetail>, IBookingDetailRepository
	{
		public BookingDetailRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
