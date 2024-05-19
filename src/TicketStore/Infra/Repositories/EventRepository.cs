using Core.Repository;
using Domain.Event;
using Domain.Event.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class EventRepository(Context context)
    : RepositoryBase<Events, Context>(context), IEventRepository
{
    public async Task<Events?> FindEvent(Guid organizerId, Guid eventId)
    {
        return await _context.Events
            .Include(e => e.Dates)
            .ThenInclude(e => e.Sectors)
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id == eventId && e.OrganizerId == organizerId);
    }
}