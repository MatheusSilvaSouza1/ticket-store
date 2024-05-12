using System.Linq.Expressions;
using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Repository
{
    public abstract class RepositoryBase<T, TContext> : IRepository<T>, IBaseRepository<T>
        where T : class, IAggregateRoot
        where TContext : DbContext, IUnitOfWork
    {
        protected readonly TContext _context;

        protected RepositoryBase(TContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async ValueTask DisposeAsync()
        {
            await DisposeAsync(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public virtual void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public virtual async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<T?> GetAsync(string id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return (await _context.Set<T>().FindAsync(id)) != null;
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        protected async virtual ValueTask DisposeAsync(bool disposing)
        {
            if (disposing)
            {
                await _context.DisposeAsync();
            }
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
    }
}