namespace Domain.Event.DTOs;

public sealed record SectorsDTO(
    string PlaceName,
    int NumberOfSeats,
    decimal Price)
{
}