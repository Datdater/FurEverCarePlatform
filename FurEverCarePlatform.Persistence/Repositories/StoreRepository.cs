using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class StoreRepository : GenericRepository<Store>, IStoreRepository
	{
		public StoreRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
