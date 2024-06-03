using Core.Domain;
using Domain.Promoter.DomainEvents;
using Domain.Promoter.DTOs;
using Domain.Promoter.ValueObjects;
using ErrorOr;

namespace Domain.Promoter;

public sealed class Promoters : AggregateRoot
{
    public string CorporateReason { get; private set; } = string.Empty;
    public Cnpj Cnpj { get; private set; }
    public string Fantasy { get; private set; }

    private Promoters()
    {
    }

    public static ErrorOr<Promoters> Register(RegisterPromoterDTO promoterDTO, bool promoterAlreadyExists)
    {
        List<Error> errors = [];

        var cnpj = Cnpj.Create(promoterDTO.Cnpj);
        errors.AddRange(cnpj.ErrorsOrEmptyList);

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
            return errors;
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