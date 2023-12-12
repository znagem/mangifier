namespace Mangifier.Api.Shared.Users.UpdateImage;

public sealed class Request
{
    public string Id { get; init; } = string.Empty;
    public FileDto Image { get; init; } = null!;
}