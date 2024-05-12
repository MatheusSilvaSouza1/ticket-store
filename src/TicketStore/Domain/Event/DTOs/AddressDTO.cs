namespace Domain.Event.DTOs;

public record AddressDTO(
    int? Number,
    string Street,
    string District,
    string City,
    string State,
    string Country)
{
}