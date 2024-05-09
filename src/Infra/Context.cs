using Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class Context : DbContext, IUnitOfWork
{
    public Context(DbContextOptions<Context> options) : base(options) { }
    
    public Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
