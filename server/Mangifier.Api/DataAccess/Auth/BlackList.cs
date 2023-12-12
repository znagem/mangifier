using MongoSharpen;

namespace Mangifier.Api.DataAccess.Auth;

[Collection("blacklists")]
internal sealed class BlackList : BaseDocument
{
    [AsObjectId]
    public string UserId { get; init; } = string.Empty;
}