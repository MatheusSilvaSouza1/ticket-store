using ErrorOr;

namespace Domain.Promoter;

public static class PromoterErrors
{
    public static Error InvalidCnpj = Error.Validation(description: "Invalid cnpj");
    public static Error InvalidCorporateReason = Error.Validation(description: "Corporate reason is required");
    public static Error PromoterAlreadyExists = Error.Validation(description: "Promoter already exists");
}