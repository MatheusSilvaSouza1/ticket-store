using Catalog.Domain.Organizer;
using Catalog.Domain.Organizer.Repositories;
using Core.Repository;

namespace Infra.Repositories;

public class OrganizerRepository(Context context)
        : RepositoryBase<Organizers, Context>(context), IOrganizerRepository
{
}