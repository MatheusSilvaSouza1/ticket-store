using Core.Mediator;
using Core.Repository;
using Domain.Organizer;
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
        modelBuilder.HasDefaultSchema("organising-api");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(Context).Assembly);
    }

    public DbSet<Organizers> Organizers { get; set; }

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
