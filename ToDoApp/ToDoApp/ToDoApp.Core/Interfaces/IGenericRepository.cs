using System.Linq.Expressions;


namespace ToDoApp.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity); 
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
        Task<IEnumerable<T>> Where(Expression<Func<T, bool>> predicate);
        IQueryable<T> AsQueryable(); 
    }
}
