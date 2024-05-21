using MediatR;

namespace Core.Messages;

public interface ICommand<TResponse> : IRequest<TResponse>
{
}