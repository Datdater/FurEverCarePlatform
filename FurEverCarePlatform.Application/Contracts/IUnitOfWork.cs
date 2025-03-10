

public interface IUnitOfWork : IDisposable
{
    public ICategoryRepository CategoryRepository { get; }
    Task<int> SaveAsync();

}