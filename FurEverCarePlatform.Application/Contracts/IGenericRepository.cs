

public interface IGenericRepository<T> where T : BaseEntity
{
    Task<T> GetByIdAsync(object id);
    Task<List<T>> GetAllAsync(string? includeProperties = null);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, string? includeProperties);
    Task<T> InsertAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, string? includeProperties = null);

    Task<Pagination<T>> GetPaginationAsync(string? includeProperties = null, int pageIndex = 0, int pageSize = 10);
    //Task GetAllAsync(Func<PetServiceStep, bool> value);
}