using Core.Domain;
using Domain.Event.DTOs;
using Domain.Event.ValueObjects;
using ErrorOr;

namespace Domain.Event;

public sealed class Events : AggregateRoot
{
    public string Name { get; private set; }
    public string Description { get; private set; }
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
    public List<Dates> Dates { get; private set; }
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
        List<Dates> dates,
        Address address)
    {
        Id = Id = Guid.NewGuid();
        OrganizerId = organizerId;
        Name = name;
        Description = description;
        Image = image;
        Dates = dates;
        Address = address;
    }

    public static ErrorOr<Events> Create(Guid organizerId, CreateEventDTO eventDTO, bool organizerExists)
    {
        List<Error> errors = [];

        if (!organizerExists)
        {
            errors.Add(EventsErrors.OrganizerDoesNotExist);
        }

        var dates = Event.Dates.Create(eventDTO.Dates);

        if (dates.IsError)
        {
            errors.AddRange(dates.ErrorsOrEmptyList);
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
            dates.Value,
            Address.Create(eventDTO.Address)
        );
    }

    public List<Error> PublishEvent(DateTime publishAt)
    {
        List<Error> errors = [];

        var firstDate = Dates.OrderBy(e => e.Start).First();

        if (publishAt >= firstDate.Start)
        {
            errors.Add(EventsErrors.EventPublishDateCannotBeAfterTheStart);
        }

        if (firstDate.Start.AddDays(-1) < publishAt)
        {
            errors.Add(EventsErrors.EventPublishDateMustBeOneDayBeforeTheStart);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        PublishAt = publishAt;
        return errors;
    }
}