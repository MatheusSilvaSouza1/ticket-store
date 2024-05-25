using Core.Repository;
using Domain.Organizer;
using Domain.Organizer.Repositories;

namespace Infra.Repositories;

public class OrganizerRepository(Context context)
        : RepositoryBase<Organizers, Context>(context), IOrganizerRepository
{
}