using Mangifier.Api.Shared;

namespace Mangifier.Api.DataAccess;

internal sealed class ImageFile
{
    public string Data { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string ContentType { get; init; } = string.Empty;
}

internal static class ImageFileMapper
{
    public static FileDto ToDto(this ImageFile image) =>
        new()
        {
            Data = image.Data,
            FileName = image.FileName,
            ContentType = image.ContentType
        };

    public static ImageFile ToEntity(this FileDto image) =>
        new()
        {
            Data = image.Data,
            FileName = image.FileName,
            ContentType = image.ContentType
        };
}