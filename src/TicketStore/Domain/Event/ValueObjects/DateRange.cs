using Domain.Event.DTOs;
using ErrorOr;

namespace Domain.Event.ValueObjects;

public record DateRange
{
    public DateTime Start { get; init; }
    public DateTime End { get; init; }

    public static ErrorOr<DateRange> Create(DateRangeDTO dateRange)
    {
        if (dateRange.Start >= dateRange.End)
        {
            return EventsErrors.InvalidEventDate;
        }

        return new DateRange()
        {
            Start = dateRange.Start,
            End = dateRange.End
        };
    }

}