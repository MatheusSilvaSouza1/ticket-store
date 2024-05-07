using Core.Repository;

namespace Core.Messages;

public abstract class CommandHandler
{
    protected static async Task<bool> PersistData(IUnitOfWork uow)
    {
        return await uow.CommitAsync();
    }
}