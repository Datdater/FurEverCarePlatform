using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    class ServiceCategoryRepository : GenericRepository<ServiceCategory>, IServiceCategoryRepository
	{
		public ServiceCategoryRepository(PetDatabaseContext context) : base(context)
		{ }

	}
}
