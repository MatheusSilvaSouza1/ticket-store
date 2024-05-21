namespace Domain.Event.DTOs;

public record EventsResponseDTO(
    Guid Id,
    string Name,
    string Description,
    string Image,
    bool IsPublished,
    List<DatesDTO> Dates,
    AddressDTO Address)
{
}