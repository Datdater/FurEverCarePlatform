using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class DeliveryRepository : GenericRepository<Delivery>, IDeliveryRepository
	{
		public DeliveryRepository(PetDatabaseContext context) : base(context)
		{
		}

	}
}
