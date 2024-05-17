namespace Domain.Event.DTOs;

public sealed record DateRangeDTO(
    DateTime Start,
    DateTime End)
{
}