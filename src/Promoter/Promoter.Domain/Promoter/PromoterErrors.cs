namespace Domain.Promoter;

public static class PromoterErrors
{
    public static Error InvalidCnpj = new Error("Invalid cnpj");
    public static Error InvalidCorporateReason = new Error("Corporate reason is required");
    public static Error PromoterAlreadyExists = new Error("Promoter already exists");
}