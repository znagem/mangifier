using Mangifier.Api.Shared;
using Mangifier.Api.Shared.Users;
using Mangifier.Api.Shared.Users.UpdateCredentials;
using MongoSharpen;
using OneOf;

namespace Mangifier.Api.DataAccess.Users.UpdateCredentials;

[RepositoryService]
public partial class Repository
{
    public async Task<OneOf<UserDto, ServiceError>> ExecuteAsync(Request req, string modifiedBy, CancellationToken ct = default)
    {
        var context = ContextFactory.Create();

        var oldUser = await context.Find<User>(x =>
                x.Match(i => i.UserName == req.CurrentUsername))
            .ExecuteFirstOrDefaultAsync(ct);

        if (oldUser is null) return new ServiceError("User not found");

        var passwordHash = HashHelper.ComputeHash(req.CurrentPassword, Convert.FromBase64String(oldUser.Key));
        if (Convert.ToBase64String(passwordHash) != oldUser.Password)
            return new ServiceError("Current password is incorrect");

        var count = await context.CountAsync<User>(x =>
            x.Match(i => i.UserName == req.NewUsername && i.Id != oldUser.Id), token: ct);

        if (count > 0) return new ServiceError("Username already exists");

        using var trans = context.Transaction();

        await context.LogAsync<User>(x => x.MatchId(oldUser.Id), ct);

        var salt = HashHelper.SaltByte();
        var password = HashHelper.ComputeHash(req.NewPassword, salt);

        var updatedUser = await context.Update<User>(x => x.MatchId(oldUser.Id))
            .Modify(x => x
                .Set(i => i.UserName, req.NewUsername)
                .Set(i => i.Password, Convert.ToBase64String(password))
                .Set(i => i.Key, Convert.ToBase64String(salt))
                .Set(i => i.ModifiedBy, new ModifiedBy { Id = modifiedBy }))
            .ExecuteAndGetAsync(ct);

        await trans.CommitAsync(ct);

        return updatedUser!.ToDto();
    }
}