using ErrorOr;

namespace Domain.Organizer.ValueObjects;

public sealed record Cnpj
{
    public string Value { get; init; }

    private Cnpj(string cnpj)
    {
        Value = cnpj;
    }

    public static ErrorOr<Cnpj> Create(string cnpj)
    {
        if (cnpj.Length != 14)
        {
            return OrganizerErrors.InvalidCnpj;
        }

        var clean = cnpj
            .Replace("-", string.Empty)
            .Replace(".", string.Empty)
            .Replace("/", string.Empty);

        return new Cnpj(clean);
    }

    public static implicit operator Cnpj(string cnpj)
    {
        return Create(cnpj).Value;
    }
}