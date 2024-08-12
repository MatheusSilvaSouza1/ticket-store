namespace Domain.Promoter;

public sealed class Promoters : AggregateRoot
{
    public string CorporateReason { get; private set; } = string.Empty;
    public Cnpj Cnpj { get; private set; }
    public string Fantasy { get; private set; }

    private Promoters()
    {
    }

    public static Result<Promoters> Register(RegisterPromoterDTO promoterDTO, bool promoterAlreadyExists)
    {
        List<IError> errors = [];
        
        var cnpj = Cnpj.Create(promoterDTO.Cnpj);
        errors.AddRange(cnpj.Errors);

        if (promoterAlreadyExists)
        {
            errors.Add(PromoterErrors.PromoterAlreadyExists);
        }

        if (string.IsNullOrWhiteSpace(promoterDTO.CorporateReason))
        {
            errors.Add(PromoterErrors.InvalidCorporateReason);
        }

        if (errors.Count > 0)
        {
            return Result.Fail(errors);
        }

        var promoter = new Promoters()
        {
            Id = Guid.NewGuid(),
            CorporateReason = promoterDTO.CorporateReason,
            Fantasy = promoterDTO.Fantasy,
            Cnpj = cnpj.Value
        };

        promoter.RaiseDomainEvent(
            new PromoterRegisteredDomainEvent(
                promoter.Id,
                promoter.Fantasy ?? promoterDTO.CorporateReason));

        return promoter;
    }
}