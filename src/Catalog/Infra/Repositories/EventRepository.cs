using Core.Repository;
using Domain.Event;
using Domain.Event.DTOs;
using Domain.Event.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class EventRepository(Context context)
    : RepositoryBase<Events, Context>(context), IEventRepository
{
    public async Task<Events?> FindEvent(Guid organizerId, Guid eventId, CancellationToken cancellationToken)
    {
        return await _context.Events
            .Include(e => e.Dates)
            .ThenInclude(e => e.Sectors)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e =>
                e.Id == eventId &&
                e.OrganizerId == organizerId, cancellationToken);
    }

    public async Task<List<EventsResponseDTO>> GetEventsDTO(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow.AddHours(-3);
        return await _context.Set<Events>()
            .Include(e => e.Dates)
            .ThenInclude(e => e.Sectors)
            .Where(e => now >= e.PublishAt)
            .Select(e => e.ToEventsResponseDTO())
            .ToListAsync(cancellationToken: cancellationToken);
    }
}