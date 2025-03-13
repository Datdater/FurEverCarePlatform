using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PetServiceStepRepository :GenericRepository<PetServiceStep>, IPetServiceStepRepository
	{
		public PetServiceStepRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
