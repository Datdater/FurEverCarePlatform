using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PetServiceRepository : GenericRepository<PetService>, IPetServiceRepository
	{
		public PetServiceRepository(PetDatabaseContext context) : base(context)
		{
		}

	}
}
