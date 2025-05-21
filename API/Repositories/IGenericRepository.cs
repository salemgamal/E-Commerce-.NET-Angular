using System.Linq.Expressions;

namespace API.Repositories
{
    public interface IGenericRepository<T>  where T : class
    {

        // Implement IRepository<T> methods to avoid interface implementation errors
        public Task<IReadOnlyList<T>> GetAllAsync();

        public Task<IReadOnlyList<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

        public Task<T> GetByIdAsync<TKey>(TKey id);

        public Task<T> GetByIdAsync<TKey>(TKey id, params Expression<Func<T, object>>[] includes);

        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(int id);
        public Task<int> CountAsync();

    }
}
