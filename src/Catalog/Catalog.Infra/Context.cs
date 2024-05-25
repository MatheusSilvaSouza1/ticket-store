using Core.Mediator;
using Core.Repository;
using Domain.Event;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class Context
    : DbContext, IUnitOfWork
{
    private readonly IMediatorHandler _mediatorHandler;
    public Context(DbContextOptions<Context> options, IMediatorHandler mediatorHandler)
        : base(options)
    {
        _mediatorHandler = mediatorHandler;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("catalog-api");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }

    public DbSet<Events> Events { get; set; }

    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default)
    {
        var success = await SaveChangesAsync(cancellationToken) > 0;
        if (success)
        {
            await _mediatorHandler.PublishEvents(this);
        }

        return success;
    }
}
