using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    class ComboRepository : GenericRepository<Combo>, IComboRepository
	{
		public ComboRepository(PetDatabaseContext context) : base(context)
		{
		}
	}
}
