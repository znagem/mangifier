namespace Mangifier.Api.Shared;

public sealed class FileDto
{
    public string Data { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
}