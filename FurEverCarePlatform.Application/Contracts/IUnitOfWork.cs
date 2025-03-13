namespace FurEverCarePlatform.Application.Contracts;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<T> GetRepository<T>()
        where T : BaseEntity;
    public ICategoryRepository CategoryRepository { get; }
    Task<int> SaveAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}
