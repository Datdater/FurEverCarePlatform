
namespace FurEverCarePlatform.Application.Contracts
{
    public interface IPetServiceRepository : IGenericRepository<PetService>
    {
         Task<PetService?> GetPetService(Guid id);
    }
}
