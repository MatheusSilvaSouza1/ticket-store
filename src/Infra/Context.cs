using Core.Repository;
using Domain.Organizer;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class Context(DbContextOptions<Context> options)
    : DbContext(options), IUnitOfWork
{
    public DbSet<Organizers> Organizers { get; set; }

    public Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
