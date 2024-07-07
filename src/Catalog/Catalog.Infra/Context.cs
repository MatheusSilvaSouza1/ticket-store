using Core.Mediator;
using Core.Messages;
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
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        => await SaveChangesAsync(cancellationToken);
}
