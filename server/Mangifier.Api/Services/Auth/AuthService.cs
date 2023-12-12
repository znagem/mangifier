using System.IdentityModel.Tokens.Jwt;
using FastEndpoints.Security;
using Mangifier.Api.DataAccess.Auth;
using Mangifier.Api.Shared.Users;

namespace Mangifier.Api.Services.Auth;

public sealed class AuthService(IConfiguration config, Repository repository)
{
    public async Task<string> CreateTokenAsync(UserDto user, CancellationToken ct = default)
    {
        var token = JWTBearer.CreateToken(
            config.GetSection("JWTKey").Value!,
            expireAt: DateTime.UtcNow.AddDays(1),
            privileges: u =>
            {
                u.Roles.Add("user");
                u.Permissions.AddRange(["user"]);
                u["id"] = user.Id;
            });

        await repository.RemoveFromBlackListAsync(user.Id, ct);

        return token;
    }

    public Task BlacklistAsync(string id, CancellationToken ct = default) => repository.BlackListAsync(id, ct);
    
    public async Task<bool> IsBlacklistedAsync(string? token, CancellationToken ct = default)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
    
        var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "id");
    
        if (claim is null) return false;
    
        return await repository.IsBlacklistedAsync(claim.Value, ct);
    }
}