using Core.Repository;
using Domain.Organizer;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class Context
    : DbContext, IUnitOfWork
{
    public Context(DbContextOptions<Context> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }

    public DbSet<Organizers> Organizers { get; set; }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}
