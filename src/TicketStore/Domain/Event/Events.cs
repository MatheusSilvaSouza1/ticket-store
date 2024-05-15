using Core.Domain;
using Domain.Event.DTOs;
using Domain.Event.ValueObjects;
using ErrorOr;

namespace Domain.Event;

public class Events : Entity, IAggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public int NumberOfSeats { get => Sectors.Sum(e => e.NumberOfSeats); }
    public string Image { get; private set; }
    public bool IsPublished
    {
        get
        {
            if (PublishAt == null || DateTime.Compare(DateTime.UtcNow.AddHours(-3), PublishAt.GetValueOrDefault()) < 0)
            {
                return false;
            }

            return true;
        }
    }
    public DateTime? PublishAt { get; set; }
    public DateRange DateRange { get; private set; }
    public List<Sectors> Sectors { get; private set; } = [];
    public Address Address { get; private set; }
    public Guid OrganizerId { get; private set; }

    private Events()
    {
    }

    private Events(
        Guid organizerId,
        string name,
        string description,
        string image,
        DateRange dateRange,
        List<Sectors> sectors,
        Address address)
    {
        OrganizerId = organizerId;
        Name = name;
        Description = description;
        Image = image;
        DateRange = dateRange;
        Sectors = sectors;
        Address = address;
    }

    public static ErrorOr<Events> Create(Guid organizerId, CreateEventDTO eventDTO, bool organizerExists)
    {
        List<Error> errors = [];

        if (!organizerExists)
        {
            errors.Add(EventsErrors.OrganizerDoesNotExist);
        }

        var dateRange = DateRange.Create(eventDTO.DateRange);

        if (dateRange.IsError)
        {
            errors.AddRange(dateRange.ErrorsOrEmptyList);
        }

        var sectors = Event.Sectors.Create(eventDTO.Sectors);
        if (sectors.IsError)
        {
            errors.AddRange(sectors.ErrorsOrEmptyList);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Events(
            organizerId,
            eventDTO.Name,
            eventDTO.Description,
            eventDTO.Image,
            dateRange.Value,
            sectors.Value,
            Address.Create(eventDTO.Address)
        );
    }

    public ErrorOr<bool> PublishEvent(DateTime publishAt)
    {
        List<Error> errors = [];

        if (publishAt >= DateRange.Start)
        {
            errors.Add(EventsErrors.EventPublishDateCannotBeAfterTheStart);
        }

        if (publishAt < DateRange.Start.AddDays(-1))
        {
            errors.Add(EventsErrors.EventPublishDateMustBeOneDayBeforeTheStart);
        }

        if (errors.Count > 0)
        {
            return false;
        }

        return true;
    }
}