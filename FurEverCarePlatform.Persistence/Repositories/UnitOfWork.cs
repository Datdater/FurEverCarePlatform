
using FurEverCarePlatform.Application.Commons.Interfaces;
using FurEverCarePlatform.Application.Commons.Services;

namespace FurEverCarePlatform.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PetDatabaseContext _context;
    private IDbContextTransaction? _transaction;
	private bool _disposed = false;
	//private readonly IClaimService _claimService;
	//private readonly ICurrentTime _currentTime;

	private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

	public IGenericRepository<T> GetRepository<T>() where T : BaseEntity
	{
		if (_repositories.TryGetValue(typeof(T), out var repository))
		{
			return (IGenericRepository<T>)repository;
		}

		var newRepository = new GenericRepository<T>(_context);
		_repositories.Add(typeof(T), newRepository);
		return newRepository;
	}

	public ICategoryRepository CategoryRepository { get; }

	public IPetServiceRepository PetServiceRepository { get; }

	public UnitOfWork(PetDatabaseContext context/*, IClaimService claimService, ICurrentTime currentTime*/)
    {
        _context = context;
		//_claimService = claimService;
		//_currentTime = currentTime;
		CategoryRepository = new CategoryRepository(_context);
		PetServiceRepository = new PetServiceRepository(_context);
	}

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }
    }
    public async Task<int> SaveAsync()
    {
		//UpdateTimestamps();
		return await _context.SaveChangesAsync();
    }
	//private void UpdateTimestamps()
	//{
	//	var entries = _context.ChangeTracker.Entries()
	//		.Where(e => e.Entity is BaseEntity &&
	//		            (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

	//	foreach (var entry in entries)
	//	{
	//		var auditable = (BaseEntity)entry.Entity;
	//		//var currentTime = _currentTime.GetCurrentTime();
	//		//var currentUser = _claimService.GetCurrentUser;

	//		if (entry.State == EntityState.Added)
	//		{
	//			auditable.CreationDate = currentTime;
	//			if (!currentUser.Equals(Guid.Empty))
	//			{
	//				auditable.CreatedBy = currentUser;
	//			}
	//		}

	//		if (entry.State == EntityState.Modified)
	//		{
	//			if (!auditable.IsDeleted)  
	//			{
	//				auditable.ModificationDate = currentTime;
	//				auditable.ModifiedBy = currentUser;
	//			}
	//			else 
	//			{
	//				auditable.DeleteDate = currentTime;
	//				auditable.DeletedBy = currentUser;
	//			}
	//		}
	//	}
	//}

	public async Task BeginTransactionAsync()
    {
	    _transaction = await _context.Database.BeginTransactionAsync();
	}

    public async Task CommitTransactionAsync()
    {
	    if (_transaction != null)
	    {
		    await _transaction.CommitAsync();
		    await _transaction.DisposeAsync();
	    }
	}

    public async Task RollbackTransactionAsync()
    {
	    if (_transaction != null)
	    {
		    await _transaction.RollbackAsync();
		    await _transaction.DisposeAsync();
	    }
	}
}