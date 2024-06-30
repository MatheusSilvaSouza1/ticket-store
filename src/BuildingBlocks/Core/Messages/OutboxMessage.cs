namespace Core.Messages;

public sealed class OutboxMessage
{
    public Guid Id { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime OcurredOnUtc { get; set; }
    public DateTime ProcessOnUtc { get; set; }
    public string? Error { get; set; }
}