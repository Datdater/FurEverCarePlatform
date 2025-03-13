using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class BookingDetailServiceRepository : GenericRepository<BookingDetailService>, IBookingDetailServiceRepository
	{
		public BookingDetailServiceRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
