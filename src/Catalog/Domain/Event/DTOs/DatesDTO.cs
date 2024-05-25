namespace Domain.Event.DTOs;

public sealed record DatesDTO(
    DateTime Start,
    DateTime End,
    List<SectorsDTO> Sectors)
{
}