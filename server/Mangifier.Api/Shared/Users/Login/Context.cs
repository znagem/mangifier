using System.ComponentModel.DataAnnotations;

namespace Mangifier.Api.Shared.Users.Login;

public sealed class Context
{
    [Required(ErrorMessage = "Required field")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Required field")]
    public string Password { get; set; } = string.Empty;
}