using Domain.Event;
using Domain.Event.DTOs;

namespace Domain.Tests.Event;

public class EventsTests
{
    [Fact]
    public void Create_ShouldReturnOrganizerDoesNotExistError_WhenOrganizerDoesntExist()
    {
        // Arrange
        var organizerId = Guid.NewGuid();
        var eventDTO = new CreateEventDTO(
            "Test Event",
            string.Empty,
            string.Empty,
            [new DatesDTO(DateTime.Now, DateTime.Now.AddHours(5), [])],
            new AddressDTO(
                1,
                "Test Street",
                "Test District",
                "Test City",
                "Test State",
                "Test Country",
                "Test Complement")
        );

        bool organizerExists = false;

        // Act
        var result = Events.Create(organizerId, eventDTO, organizerExists);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(EventsErrors.OrganizerDoesNotExist, result.Errors.First());
    }

    [Fact]
    public void Create_ShouldReturnInvalidEventDateError_WhenStartDateAfterEndDate()
    {
        // Arrange
        var organizerId = Guid.NewGuid();
        var eventDTO = new CreateEventDTO(
            "Test Event",
            string.Empty,
            string.Empty,
            [new DatesDTO(DateTime.Now.AddDays(1), DateTime.Now, [])],
            new AddressDTO(
                1,
                "Test Street",
                "Test District",
                "Test City",
                "Test State",
                "Test Country",
                "Test Complement")
        );

        bool organizerExists = true;

        // Act
        var result = Events.Create(organizerId, eventDTO, organizerExists);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(EventsErrors.EventInvalidDate, result.Errors.First());
    }

    [Fact]
    public void Create_ShouldReturnSectorNumberSeatsGreaterThanZeroError_WhenSectorHasZeroSeats()
    {
        // Arrange
        var organizerId = Guid.NewGuid();
        var eventDTO = new CreateEventDTO(
            "Test Event",
            string.Empty,
            string.Empty,
            [new DatesDTO(
                DateTime.Now,
                DateTime.Now.AddHours(5),
                [new SectorsDTO("Sector 1", 0, 1)])],
            new AddressDTO(
                1,
                "Test Street",
                "Test District",
                "Test City",
                "Test State",
                "Test Country",
                "Test Complement")
        );

        bool organizerExists = true;

        // Act
        var result = Events.Create(organizerId, eventDTO, organizerExists);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Description == EventsErrors.EventSectorNumberSeatsGreaterThanZero.Description);
    }

    [Fact]
    public void Create_ShouldReturnSectorPlaceNameIsRequiredError_WhenSectorHasEmptyPlaceName()
    {
        // Arrange
        var organizerId = Guid.NewGuid();
        var eventDTO = new CreateEventDTO(
            "Test Event",
            string.Empty,
            string.Empty,
            [new DatesDTO(
                DateTime.Now,
                DateTime.Now.AddHours(5),
                [new SectorsDTO("", 10, 1)])],
            new AddressDTO(
                1,
                "Test Street",
                "Test District",
                "Test City",
                "Test State",
                "Test Country",
                "Test Complement")
        );

        bool organizerExists = true;

        // Act
        var result = Events.Create(organizerId, eventDTO, organizerExists);

        // Assert
        Assert.True(result.IsError);
        Assert.Contains(result.Errors, error => error.Description == EventsErrors.EventSectorPlaceNameIsRequired.Description);
    }

    [Fact]
    public void Create_ShouldCreateEvent_WhenAllDataIsValid()
    {
        // Arrange
        var organizerId = Guid.NewGuid();

        var eventDTO = new CreateEventDTO(
            "Test Event",
            "Test Event Description",
            "url",
            [new DatesDTO(
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(2),
                [new SectorsDTO("Sector 1", 10, 1)])],
            new AddressDTO(
                1,
                "Test Street",
                "Test District",
                "Test City",
                "Test State",
                "Test Country",
                "Test Complement")
        );
        bool organizerExists = true;

        // Act
        var result = Events.Create(organizerId, eventDTO, organizerExists);

        // Assert
        Assert.False(result.IsError);

        var evt = result.Value;
        Assert.Equal(organizerId, evt.OrganizerId);
        Assert.Equal(eventDTO.Name, evt.Name);
        Assert.Equal(eventDTO.Description, evt.Description);
        Assert.Equal(eventDTO.Image, evt.Image);
        Assert.Equal(eventDTO.Address.Street, evt.Address.Street);
        Assert.Equal(eventDTO.Address.City, evt.Address.City);
    }
}