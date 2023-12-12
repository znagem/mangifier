using Mangifier.Api.Shared.Users;
using Mangifier.Api.Shared.Users.UpdateImage;
using MongoSharpen;
using OneOf;

namespace Mangifier.Api.DataAccess.Users.UpdateImage;

[RepositoryService]
public partial class Repository
{
    public async Task<OneOf<UserDto, ServiceError>> ExecuteAsync(Request request, string modifiedBy,
        CancellationToken ct = default)
    {
        var context = ContextFactory.Create();

        var oldUser = await context.Find<User>(x => x.MatchId(request.Id)).ExecuteFirstOrDefaultAsync(ct);
        if (oldUser is null) return new ServiceError("User not found");

        using var trans = context.Transaction();

        var user = await context.Update<User>(x => x.MatchId(request.Id))
            .Modify(x => x
                .Set(i => i.Image, request.Image.ToEntity())
                .Set(i => i.ModifiedBy, new ModifiedBy { Id = modifiedBy }))
            .ExecuteAndGetAsync(ct);

        await trans.CommitAsync(ct);

        return user!.ToDto();
    }
}