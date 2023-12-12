using System.ComponentModel.DataAnnotations;

namespace Mangifier.Api.Shared.Users.UpdateCredentials;

public sealed class Context
{
    [Required(ErrorMessage = "Required field")]
    public string CurrentUsername { get; set; } = string.Empty;

    [Required(ErrorMessage = "Required field")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Required field")]
    public string NewUsername { get; set; } = string.Empty;

    [Required(ErrorMessage = "Required field")]
    [MinLength(5, ErrorMessage = "Password is too short")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Required field")]
    [Compare(nameof(NewPassword), ErrorMessage = "Password does not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}