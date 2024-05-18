namespace Domain.Event.DTOs;

public sealed record CreateEventDTO(
    string Name,
    string Description,
    string Image,
    DateRangeDTO DateRange,
    List<SectorsDTO> Sectors,
    AddressDTO Address)
{
}