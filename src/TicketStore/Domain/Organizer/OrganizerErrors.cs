using ErrorOr;

namespace Domain.Organizer;

public static class OrganizerErrors
{
    public static Error InvalidCnpj = Error.Validation(description: "Invalid cnpj");
    public static Error InvalidCorporateReason = Error.Validation(description: "Corporate reason is required");
}