using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurEverCarePlatform.Persistence.Repositories
{
    public class PetServiceRepository : GenericRepository<PetService>, IPetServiceRepository
	{
		private readonly PetDatabaseContext _context;
		public PetServiceRepository(PetDatabaseContext context) : base(context)
		{
			_context = context;
		}

		public async Task<PetService?> GetPetService(Guid id)
		{
			return await _context.PetServices
				.Include(p => p.PetServiceDetails)
				.Include(p => p.PetServiceSteps)
				.FirstOrDefaultAsync(p => p.Id == id)
				;
		}
	}
}
