using System.ComponentModel.DataAnnotations;

namespace FantasyAssistant.Application.Internal;

public sealed class ApiSettings
{
    [Url]
    [Required]
    public string BaseAddress { get; set; } = string.Empty;

    [Required]
    public string Bootstrap { get; set; } = string.Empty;

    [Required]
    public string Fixtures { get; set; } = string.Empty;

    [Required]
    public string Team { get; set; } = string.Empty;
}