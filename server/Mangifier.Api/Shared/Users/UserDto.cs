using System.Text.Json.Serialization;

namespace Mangifier.Api.Shared.Users;

public sealed class UserDto
{
    public string Id { get; init; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    [JsonIgnore]
    public string FullName => $"{FirstName} {LastName}";

    public string Role { get; set; } = "Non-Admin";

    public FileDto? Image { get; set; }
}