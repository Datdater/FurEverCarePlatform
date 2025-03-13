using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PetServiceDetailRepository : GenericRepository<PetServiceDetail>, IPetServiceDetailRepository
	{
		public PetServiceDetailRepository(PetDatabaseContext context) : base(context)
		{ }
    }
}
