using MediatR;

namespace Core.Cqrs;

public abstract class Command<T> : IRequest<T>
{
}