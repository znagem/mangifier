namespace Mangifier.Api.Shared.Users.UpdateCredentials;

public sealed class Request
{
    public string Id { get; init; } = string.Empty;
    public string CurrentUsername { get; init; } = string.Empty;
    public string CurrentPassword { get; init; } = string.Empty;
    public string NewUsername { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}