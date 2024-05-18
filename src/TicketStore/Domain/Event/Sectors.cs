using Core.Domain;
using Domain.Event.DTOs;
using ErrorOr;

namespace Domain.Event;

public sealed class Sectors : Entity
{
    public string PlaceName { get; private set; }
    public int NumberOfSeats { get; private set; }
    public Guid EventId { get; private set; }
    public Events Event { get; private set; }

    private Sectors()
    {
    }

    private Sectors(string placeName, int numberOfSeats)
    {
        Id = Guid.NewGuid();
        PlaceName = placeName;
        NumberOfSeats = numberOfSeats;
    }

    public static ErrorOr<List<Sectors>> Create(List<SectorsDTO> sectorsDTOs)
    {
        List<Error> errors = [];
        List<Sectors> sectors = [];

        foreach (var sectorDTO in sectorsDTOs)
        {
            var sector = Create(sectorDTO);

            if (sector.IsError)
            {
                errors.AddRange(sector.ErrorsOrEmptyList);
            }
            else
            {
                sectors.Add(sector.Value);
            }
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return sectors;
    }

    public static ErrorOr<Sectors> Create(SectorsDTO sectorsDTO)
    {
        List<Error> errors = [];

        if (sectorsDTO.NumberOfSeats <= 0)
        {
            errors.Add(EventsErrors.EventSectorNumberSeatsGreaterThanZero);
        }

        if (string.IsNullOrWhiteSpace(sectorsDTO.PlaceName))
        {
            errors.Add(EventsErrors.EventSectorPlaceNameIsRequired);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Sectors(sectorsDTO.PlaceName, sectorsDTO.NumberOfSeats);
    }
}