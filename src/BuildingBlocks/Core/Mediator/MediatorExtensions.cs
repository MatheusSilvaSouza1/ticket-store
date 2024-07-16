using Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Core.Mediator;

public static class MediatorExtensions
{
    // public static async Task PublishEvents<T>(this IMediatorHandler mediator, T ctx)
    //     where T : DbContext
    // {
    //     var domainEntities = ctx.ChangeTracker
    //                 .Entries<AggregateRoot>()
    //                 .Where(e => e.Entity.DomainEvents.Count > 0)
    //                 .ToList();

    //     var domainEvents = domainEntities
    //         .SelectMany(x => x.Entity.DomainEvents)
    //         .ToList();

    //     domainEntities.ToList()
    //             .ForEach(entity => entity.Entity.ClearEvent());

    //     var tasks = domainEvents
    //             .Select(async (domainEvent) =>
    //             {
    //                 await mediator.PublishMessage(domainEvent);
    //             });

    //     await Task.WhenAll(tasks);
    // }
}