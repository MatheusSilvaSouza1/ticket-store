using Core.Domain;
using Core.Messages;

namespace Domain.Promoter.DomainEvents;

public sealed class PromoterRegisteredDomainEvent : Message, IDomainEvent
{
    public Guid PromoterId { get; init; }
    public string PromoterName { get; init; }

    public PromoterRegisteredDomainEvent(Guid promoterId, string promoterName)
    {
        PromoterId = promoterId;
        PromoterName = promoterName;
    }
}