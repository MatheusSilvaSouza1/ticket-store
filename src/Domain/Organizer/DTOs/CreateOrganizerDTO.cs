using System.ComponentModel.DataAnnotations;

namespace Domain.Organizer.DTOs;

public sealed class CreateOrganizerDTO
{
    [Required]
    public string CorporateReason { get; set; } = string.Empty;
    [Required]
    public string Cnpj { get; set; } = string.Empty;
    public string Fantasy { get; set; } = string.Empty;
}