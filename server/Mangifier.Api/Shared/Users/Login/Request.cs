using FastEndpoints;

namespace Mangifier.Api.Shared.Users.Login;

public sealed class Request
{
    [QueryParam]
    [BindFrom("username")]
    public string Username { get; init; } = string.Empty;

    [QueryParam]
    [BindFrom("password")]
    public string Password { get; init; } = string.Empty;
}