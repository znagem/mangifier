using Mangifier.Api.Shared.Users;
using Mangifier.Api.Shared.Users.UpdateInfo;
using MongoDB.Driver;
using MongoSharpen;
using OneOf;

namespace Mangifier.Api.DataAccess.Users.UpdateInfo;

[RepositoryService]
public partial class Repository
{
    public async Task<OneOf<UserDto, ServiceError>> ExecuteAsync(Request request,
        string modifiedBy, CancellationToken ct = default)
    {
        var context = ContextFactory.Create();

        using var trans = context.Transaction();

        var filter = Builders<User>.Filter.Match(t =>
            t.Id != request.Id &&
            t.FirstName.ToLower() == request.FirstName.ToLower() &&
            t.LastName.ToLower() == request.LastName.ToLower());

        var count = await context.CountAsync(filter, token: ct);
        if (count > 0) return new ServiceError("User already exists");

        var exist = await context.CountAsync<User>(x => x.MatchId(request.Id), token: ct);
        if (exist == 0) return new ServiceError("User not found");

        await context.LogAsync<User>(x => x.MatchId(request.Id), ct);

        var user = await context.Update<User>(x => x.MatchId(request.Id))
            .Modify(x => x
                .Set(i => i.FirstName, request.FirstName)
                .Set(i => i.LastName, request.LastName)
                .Set(i => i.FullName, $"{request.FirstName} {request.LastName}")
                .Set(i => i.ModifiedBy, new ModifiedBy { Id = modifiedBy }))
            .ExecuteAndGetAsync(ct);

        await trans.CommitAsync(ct);

        return user!.ToDto();
    }
}