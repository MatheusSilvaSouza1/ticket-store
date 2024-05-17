namespace Domain.Event.DTOs;

public sealed record AddressDTO(
    int? Number,
    string Street,
    string District,
    string City,
    string State,
    string Country)
{
}