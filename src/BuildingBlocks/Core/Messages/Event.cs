using MediatR;

namespace Core.Messages;

public abstract class Event : Message, INotification
{
}