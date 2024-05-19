using Domain.Event.DTOs;

namespace Domain.Event.ValueObjects;

public record Address(
    int? Number,
    string Street,
    string District,
    string City,
    string State,
    string Country,
    string? Complement)
{
    public static Address Create(AddressDTO addressDTO)
    {
        return new Address(
            addressDTO.Number,
            addressDTO.Street,
            addressDTO.District,
            addressDTO.City,
            addressDTO.State,
            addressDTO.Country,
            addressDTO.Complement);
    }
}