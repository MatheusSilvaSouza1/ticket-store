using ErrorOr;

namespace Domain.Event;

public static class EventsErrors
{
    public static Error OrganizerDoesNotExist = Error.Validation(description: "Organizer does not exist");
    public static Error EventAlreadyExists = Error.Validation(description: "Event already exists");
    public static Error EventInvalidDate = Error.Validation(description: "Invalid event date");
    public static Error EventSectorNumberSeatsGreaterThanZero = Error.Validation(description: "Number of seats per sector must be greater than zero");
    public static Error EventSectorPlaceNameIsRequired = Error.Validation(description: "Place name is required");
    public static Error EventPublishDateCannotBeAfterTheStart = Error.Validation(description: "The event publication date cannot be after the start of the event");
    public static Error EventPublishDateMustBeOneDayBeforeTheStart = Error.Validation(description: "The publication of the event must be one day before the start of the event");
}