using Core.Domain;
using Domain.Organizer.DTOs;
using Domain.Organizer.ValueObjects;
using ErrorOr;

namespace Domain.Organizer;

public sealed class Organizers : Entity, IAggregateRoot
{
    public string CorporateReason { get; private set; } = string.Empty;
    public Cnpj? Cnpj { get; init; }
    public string Fantasy { get; private set; } = string.Empty;

    private Organizers()
    {
    }

    public static ErrorOr<Organizers> Register(RegisterOrganizerDTO organizerDTO)
    {
        List<Error> errors = [];

        var cnpj = Cnpj.Create(organizerDTO.Cnpj);
        errors.AddRange(cnpj.ErrorsOrEmptyList);

        if (string.IsNullOrWhiteSpace(organizerDTO.CorporateReason))
        {
            errors.Add(OrganizerErrors.InvalidCorporateReason);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Organizers()
        {
            CorporateReason = organizerDTO.CorporateReason,
            Fantasy = organizerDTO.Fantasy,
            Cnpj = cnpj.Value
        };
    }
}