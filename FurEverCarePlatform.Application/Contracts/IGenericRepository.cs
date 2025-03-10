using FurEverCarePlatform.Application.Commons;
using System.Linq.Expressions;


public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(object id);
    Task<List<T>> GetAllAsync(string? includeProperties = null);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, string? includeProperties);
    Task<T> InsertAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);

    Task<Pagination<T>> GetPaginationAsync(string? includeProperties = null, int pageIndex = 0, int pageSize = 10);
}