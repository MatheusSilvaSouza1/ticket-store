using ErrorOr;

namespace Domain.Event;

public static class EventsErrors
{
    public static readonly Error OrganizerDoesNotExist = Error.Validation(
        description: "Organizer does not exist");

    public static readonly Error EventDoesNotExist = Error.Validation(
        description: "Event does not exist");

    public static readonly Error EventAlreadyExists = Error.Validation(
        description: "Event already exists");

    public static readonly Error EventInvalidDate = Error.Validation(
        description: "Invalid event date");

    public static readonly Error EventSectorNumberSeatsGreaterThanZero = Error.Validation(
        description: "Number of seats per sector must be greater than zero");

    public static readonly Error EventSectorPlaceNameIsRequired = Error.Validation(
        description: "Place name is required");

    public static readonly Error EventPublishDateCannotBeAfterTheStart = Error.Validation(
        description: "The event publication date cannot be after the start of the event");

    public static readonly Error EventPublishDateMustBeOneDayBeforeTheStart = Error.Validation(
        description: "The publication of the event must be one day before the start of the event");

}