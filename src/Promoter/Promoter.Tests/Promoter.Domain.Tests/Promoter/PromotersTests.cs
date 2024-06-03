public class PromotersTests
{
    [Fact]
    public void Register_ShouldReturnError_WhenCnpjIsInvalid()
    {
        // Arrange
        var promoterDTO = new RegisterPromoterDTO
        {
            Cnpj = "", // Invalid CNPJ (should be 14 digits)
            CorporateReason = "Razão Social Exemplo",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Promoters.Register(promoterDTO, false);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(PromoterErrors.InvalidCnpj, result.Errors.Single());
    }

    [Fact]
    public void Register_ShouldReturnError_WhenCorporateReasonIsMissing()
    {
        // Arrange
        var promoterDTO = new RegisterPromoterDTO
        {
            Cnpj = "12345678901234",
            CorporateReason = "",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Promoters.Register(promoterDTO, false);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(PromoterErrors.InvalidCorporateReason, result.Errors.Single());
    }

    [Fact]
    public void Register_ShouldReturnError_WhenPromoterAlreadyExists()
    {
        // Arrange
        var promoterDTO = new RegisterPromoterDTO
        {
            Cnpj = "12345678901234",
            CorporateReason = "Razão Social Exemplo",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Promoters.Register(promoterDTO, true);

        // Assert
        Assert.True(result.IsError);
        Assert.Single(result.Errors);
        Assert.Equal(PromoterErrors.PromoterAlreadyExists, result.Errors.Single());
    }

    [Fact]
    public void Register_ShouldReturnPromoter_WhenValidData()
    {
        // Arrange
        var promoterDTO = new RegisterPromoterDTO
        {
            Cnpj = "12345678901234",
            CorporateReason = "Razão Social Exemplo",
            Fantasy = "Nome Fantasia Exemplo",
        };

        // Act
        var result = Promoters.Register(promoterDTO, false);

        // Assert
        Assert.False(result.IsError);
        var promoter = result.Value;
        Assert.Equal(promoterDTO.CorporateReason, promoter.CorporateReason);
        Assert.Equal(promoterDTO.Fantasy, promoter.Fantasy);
        Assert.Equal("12345678901234", promoter.Cnpj);
    }
}