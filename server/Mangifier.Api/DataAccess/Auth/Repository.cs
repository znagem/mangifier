using MongoSharpen;

namespace Mangifier.Api.DataAccess.Auth;

[RepositoryService]
public partial class Repository
{
    public async Task BlackListAsync(string id, CancellationToken ct = default)
    {
        var context = ContextFactory.Create();
        var count = await context.CountAsync<BlackList>(x => x.Match(i => i.UserId == id), token: ct);
        if (count > 0) return;

        var newToken = new BlackList { UserId = id };
        await context.SaveAsync(newToken, ct);
    }

    public async Task RemoveFromBlackListAsync(string id, CancellationToken ct = default)
    {
        var context = ContextFactory.Create();
        await context.Delete<BlackList>(x => x.Match(i => i.UserId == id)).ExecuteOneAsync(true, ct);
    }

    public async Task<bool> IsBlacklistedAsync(string id, CancellationToken ct = default)
    {
        var context = ContextFactory.Create();
        var count = await context.CountAsync<BlackList>(x => x.Match(i => i.UserId == id), token: ct);

        return count > 0;
    }
}