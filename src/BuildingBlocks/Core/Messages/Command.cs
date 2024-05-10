using MediatR;

namespace Core.Messages;

public abstract class Command<TOut> : Message, IRequest<TOut>
{
}