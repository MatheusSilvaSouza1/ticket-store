using Domain.Organizer;
using Domain.Organizer.DTOs;

namespace Domain.Tests.Organizer;

public class OrganizersTests
{
    [Fact]
    public void Register_ShouldReturnError_WhenCnpjIsInvalid()
    {
        // Arrange
        var organizerDTO = new RegisterOrganizerDTO
        {
            Cnpj = "", // Invalid CNPJ (should be 14 digits)
            CorporateReason = "Razão Social Exemplo",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Organizers.Register(organizerDTO, false);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(OrganizerErrors.InvalidCnpj, result.Errors.Single());
    }

    [Fact]
    public void Register_ShouldReturnError_WhenCorporateReasonIsMissing()
    {
        // Arrange
        var organizerDTO = new RegisterOrganizerDTO
        {
            Cnpj = "12345678901234",
            CorporateReason = "",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Organizers.Register(organizerDTO, false);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(OrganizerErrors.InvalidCorporateReason, result.Errors.Single());
    }

    [Fact]
    public void Register_ShouldReturnError_WhenOrganizerAlreadyExists()
    {
        // Arrange
        var organizerDTO = new RegisterOrganizerDTO
        {
            Cnpj = "12345678901234",
            CorporateReason = "Razão Social Exemplo",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Organizers.Register(organizerDTO, true);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(OrganizerErrors.OrganizerAlreadyExists, result.Errors.Single());
    }

    [Fact]
    public void Register_ShouldReturnOrganizer_WhenValidData()
    {
        // Arrange
        var organizerDTO = new RegisterOrganizerDTO
        {
            Cnpj = "12345678901234",
            CorporateReason = "Razão Social Exemplo",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Organizers.Register(organizerDTO, false);

        // Assert
        Assert.False(result.IsError);
        var organizer = result.Value;
        Assert.Equal(organizerDTO.CorporateReason, organizer.CorporateReason);
        Assert.Equal(organizerDTO.Fantasy, organizer.Fantasy);
        Assert.Equal("12345678901234", organizer.Cnpj);
    }
}