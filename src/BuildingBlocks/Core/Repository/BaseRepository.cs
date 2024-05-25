using System.Linq.Expressions;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository
{
    public abstract class RepositoryBase<T, TContext> : IRepository<T>, IBaseRepository<T>
        where T : AggregateRoot
        where TContext : DbContext, IUnitOfWork
    {
        protected readonly TContext _context;

        protected RepositoryBase(TContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<T?> GetAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().AnyAsync(expression);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public virtual void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> items)
        {
            _context.Set<T>().UpdateRange(items);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }
    }
}