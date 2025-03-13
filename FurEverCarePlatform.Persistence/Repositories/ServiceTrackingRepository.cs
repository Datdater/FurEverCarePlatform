using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class ServiceTrackingRepository : GenericRepository<ServiceTracking>, IServiceTrackingRepository
	{
		public ServiceTrackingRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
