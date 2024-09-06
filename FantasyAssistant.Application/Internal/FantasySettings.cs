using System.ComponentModel.DataAnnotations;

namespace FantasyAssistant.Application.Internal;

public sealed class FantasySettings
{
    [Required]
    public string TeamId { get; set; } = string.Empty;

    [Range(1, 38)]
    [Required]
    public string LastGameWeek { get; set; } = string.Empty;
}