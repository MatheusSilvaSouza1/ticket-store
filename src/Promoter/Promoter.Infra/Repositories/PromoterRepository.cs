using Core.Repository;
using Domain.Promoter;
using Domain.Promoter.Repositories;

namespace Infra.Repositories;

public class PromoterRepository(Context context)
        : RepositoryBase<Promoters, Context>(context), IPromoterRepository
{
}