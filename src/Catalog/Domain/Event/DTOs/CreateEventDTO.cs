namespace Domain.Event.DTOs;

public sealed record CreateEventDTO(
    string Name,
    string Description,
    string Image,
    List<DatesDTO> Dates,
    AddressDTO Address)
{
}