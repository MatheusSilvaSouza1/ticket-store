using Core.Messages;

namespace Core.Domain;

public abstract class AggregateRoot : Entity
{
    public IReadOnlyCollection<Event> DomainEvents => _domainEvents.AsReadOnly();
    private List<Event> _domainEvents = [];

    protected void RaiseDomainEvent(Event domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearEvent()
    {
        _domainEvents = [];
    }
}