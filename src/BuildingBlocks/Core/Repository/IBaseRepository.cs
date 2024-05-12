using System.Linq.Expressions;
using Core.Domain;

namespace Core.Repository
{
    public interface IBaseRepository<T> : IRepository<T>
        where T : class, IAggregateRoot
    {
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<T?> GetAsync(string id);
        Task<T?> GetAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> items);
        void Create(T entity);
        void Delete(T entity);
    }
}