using Core.Repository;
using Domain.Event;
using Domain.Event.Repositories;

namespace Infra.Repositories;

public class EventRepository(Context context)
    : RepositoryBase<Events, Context>(context), IEventRepository
{
}