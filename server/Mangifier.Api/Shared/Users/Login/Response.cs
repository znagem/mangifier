namespace Mangifier.Api.Shared.Users.Login;

public sealed class Response
{
    public string Token { get; init; } = string.Empty;
    public UserDto User { get; init; } = null!;
}