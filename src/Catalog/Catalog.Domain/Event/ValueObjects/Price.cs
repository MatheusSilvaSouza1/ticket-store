using Domain.Event;
using ErrorOr;

namespace Catalog.Domain.Event.ValueObjects;

public record Price
{
    public decimal Value { get; init; }

    private Price(decimal value)
    {
        Value = value;
    }

    public static ErrorOr<Price> Create(decimal value)
    {
        if (value <= 0)
        {
            return EventsErrors.EventPriceGreaterThanZero;
        }

        return new Price(value);
    }
}