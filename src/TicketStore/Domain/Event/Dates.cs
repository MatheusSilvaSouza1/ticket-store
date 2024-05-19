using Core.Domain;
using Domain.Event.DTOs;
using ErrorOr;

namespace Domain.Event;

public sealed class Dates : Entity
{
    public DateTime Start { get; private set; }
    public DateTime End { get; private set; }
    public int NumberOfSeats { get => Sectors.Sum(e => e.NumberOfSeats); }
    public List<Sectors> Sectors { get; private set; } = [];
    public Guid EventId { get; private set; }
    public Events Event { get; private set; }

    private Dates()
    {
    }

    public static ErrorOr<List<Dates>> Create(List<DatesDTO> dates)
    {
        List<Error> errors = [];
        List<Dates> datesDomain = [];

        foreach (var date in dates)
        {
            var result = Create(date);
            if (result.IsError)
            {
                errors.AddRange(result.ErrorsOrEmptyList);
            }
            else
            {
                datesDomain.Add(result.Value);
            }
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return datesDomain;
    }

    public static ErrorOr<Dates> Create(DatesDTO dateRange)
    {
        List<Error> errors = [];
        if (dateRange.Start >= dateRange.End)
        {
            errors.Add(EventsErrors.EventInvalidDate);
        }

        var sectors = Domain.Event.Sectors.Create(dateRange.Sectors);

        if (sectors.IsError)
        {
            errors.AddRange(sectors.Errors);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Dates()
        {
            Id = Guid.NewGuid(),
            Start = dateRange.Start,
            End = dateRange.End,
            Sectors = sectors.Value
        };
    }
}