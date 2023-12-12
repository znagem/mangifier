using Mangifier.Api.Shared;

namespace Mangifier.Api.DataAccess.Users.Seed;

[RepositoryService]
public partial class Repository
{
    public async Task ExecuteAsync(CancellationToken ct = default)
    {
        var context = ContextFactory.Create();
        var count = await context.CountEstimatedAsync<User>(ct);
        if (count > 0) return;

        const string defaultUser = "user";
        
        var salt = HashHelper.SaltByte();
        var password = HashHelper.ComputeHash(defaultUser, salt);

        var user = new User
        {
            FirstName = "Default",
            LastName = "User",
            FullName = "Default User",
            UserName = defaultUser,
            Key = Convert.ToBase64String(salt),
            Password = Convert.ToBase64String(password),
        };

        await context.SaveAsync(user, ct);
    }
}