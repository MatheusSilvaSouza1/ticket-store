namespace Core.Messages;

public abstract class Message
{
    public string MessageType { get; init; }
    public DateTime Timestamp { get; init; }

    protected Message()
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.UtcNow.AddHours(-3);
    }
}