namespace Domain.Promoter.ValueObjects;

public sealed record Cnpj
{
    public string Value { get; init; }

    private Cnpj(string cnpj)
    {
        Value = cnpj;
    }

    public static Result<Cnpj> Create(string cnpj)
    {
        var clean = cnpj
            .Replace("-", string.Empty)
            .Replace(".", string.Empty)
            .Replace("/", string.Empty);

        if (clean.Length != 14)
        {
            return Result.Fail(PromoterErrors.InvalidCnpj);
        }

        return Result.Ok(new Cnpj(clean));
    }

    public static implicit operator Cnpj(string cnpj)
    {
        return Create(cnpj).Value;
    }
}