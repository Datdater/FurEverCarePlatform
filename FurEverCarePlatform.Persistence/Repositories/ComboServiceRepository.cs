using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class ComboServiceRepository : GenericRepository<ComboService>, IComboServiceRepository
	{
		public ComboServiceRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
