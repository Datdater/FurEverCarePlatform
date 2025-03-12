
public interface IUnitOfWork : IDisposable
{
    public ICategoryRepository CategoryRepository { get; }
    Task<int> SaveAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

	Task RollbackTransactionAsync();


}