using MediatR;

namespace Core.Messages;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}