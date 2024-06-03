namespace Contracts;

public record PromoterRegisteredIntegrationEvent(
    Guid PromoterId,
    string PromoterName) : IntegrationEvent
{
}