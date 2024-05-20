using Core.Messages;

namespace Core.Domain;

public abstract class AggregateRoot : Entity
{
    public IReadOnlyCollection<Message> DomainEvents => _domainEvents.AsReadOnly();
    private List<Message> _domainEvents = [];

    protected void RaiseDomainEvent(Message domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearEvent()
    {
        _domainEvents = [];
    }
}