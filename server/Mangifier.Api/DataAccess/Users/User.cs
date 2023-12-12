using Mangifier.Api.Shared.Users;
using MongoSharpen;

namespace Mangifier.Api.DataAccess.Users;

[Collection("users")]
internal sealed class User : BaseDocument
{
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string FullName { get; init; } = string.Empty;
    public string UserName { get; init; } = null!;
    public string Key { get; init; } = null!;
    public string Password { get; init; } = null!;
    public ImageFile? Image { get; set; }
}

internal static class UserMapper
{
    public static UserDto ToDto(this User user) =>
        new()
        {
            Id = user.Id,
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Image = user.Image?.ToDto()
        };
}