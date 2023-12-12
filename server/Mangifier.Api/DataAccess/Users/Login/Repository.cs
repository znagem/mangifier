using Mangifier.Api.Shared;
using Mangifier.Api.Shared.Users;
using Mangifier.Api.Shared.Users.Login;
using MongoSharpen;
using OneOf;

namespace Mangifier.Api.DataAccess.Users.Login;

[RepositoryService]
public partial class Repository
{
    public async Task<OneOf<UserDto, ServiceError>> ExecuteAsync(Request request,
        CancellationToken ct = default)
    {
        var context = ContextFactory.Create();
        var user = await context.Find<User>(x => x.Match(t => t.UserName == request.Username))
            .ExecuteFirstOrDefaultAsync(ct);
        if (user is null) return new ServiceError("User not found");

        var salt = Convert.FromBase64String(user.Key);
        var currentPassword = Convert.ToBase64String(HashHelper.ComputeHash(request.Password, salt));
        if (currentPassword != user.Password) return new ServiceError("Incorrect password");

        return user.ToDto();
    }
}