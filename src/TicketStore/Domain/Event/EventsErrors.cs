using ErrorOr;

namespace Domain.Event;

public static class EventsErrors
{
    public static Error EventAlreadyExists = Error.Validation(description: "Event already exists");
    public static Error OrganizerDoesNotExist = Error.Validation(description: "Organizer does not exist");
    public static Error InvalidEventDate = Error.Validation(description: "Invalid event date");
    public static Error SectorNumberSeatsGreaterThanZero = Error.Validation(description: "Number of seats per sector must be greater than zero");
    public static Error SectorPlaceNameIsRequired = Error.Validation(description: "Plane name is required");

}