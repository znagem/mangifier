using Mangifier.Api.Shared.Users;
using MongoSharpen;
using OneOf;

namespace Mangifier.Api.DataAccess.Users.Get;

[RepositoryService]
public partial class Repository
{
    public async Task<OneOf<UserDto, ServiceError>> ExecuteAsync(string id, CancellationToken ct)
    {
        var context = ContextFactory.Create();
        var user = await context.Find<User>(x => x.MatchId(id))
            .ExecuteFirstOrDefaultAsync(ct);

        if (user is null) return new ServiceError("User not found");

        return user.ToDto();
    }
}