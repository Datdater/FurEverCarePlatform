
using FurEverCarePlatform.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GetRepository<T>() where T : BaseEntity;
	public ICategoryRepository CategoryRepository { get; }

    public IPetServiceRepository PetServiceRepository { get; }
	Task<int> SaveAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

	Task RollbackTransactionAsync();

}