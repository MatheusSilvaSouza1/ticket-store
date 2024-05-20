using System.ComponentModel.DataAnnotations.Schema;
using MediatR;

namespace Core.Messages;

[NotMapped]
public abstract class Message : INotification
{
    public string MessageType { get; init; }
    public DateTime Timestamp { get; init; }

    protected Message()
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.UtcNow.AddHours(-3);
    }
}