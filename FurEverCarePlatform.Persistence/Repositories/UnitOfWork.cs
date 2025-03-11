using FurEverCarePlatform.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace FurEverCarePlatform.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly PetDatabaseContext _context;
    private readonly IDbContextTransaction? _transaction;
	private bool _disposed = false;
    public ICategoryRepository CategoryRepository { get; }

    public UnitOfWork(PetDatabaseContext context, IDbContextTransaction transaction)
    {
        _context = context;
		_transaction = transaction;
		CategoryRepository = new CategoryRepository(_context);
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
        foreach (var entry in _context.ChangeTracker.Entries<BaseEntity>())
        {
            // switch (entry.State)
            // {
            //     case EntityState.Added:
            //         entry.Entity.Created = DateTime.Now;
            // }
        }
        return await _context.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
	    await _context.Database.BeginTransactionAsync();
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