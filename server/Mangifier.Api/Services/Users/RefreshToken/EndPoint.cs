using FastEndpoints;
using Mangifier.Api.DataAccess.Users.Get;
using Mangifier.Api.Services.Auth;
using Mangifier.Api.Shared.Users.Login;

namespace Mangifier.Api.Services.Users.RefreshToken;

public sealed class EndPoint(IRepositoryFactory factory, AuthService authService) : EndpointWithoutRequest<Response>
{
    public override void Configure()
    {
        Get("users/refresh-token");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = User.Claims.First(i => i.Type == "id").Value;

        var repository = factory.Create<Repository>();

        var result = await repository.ExecuteAsync(id, ct);

        await result.Match<Task>(
            async user =>
            {
                var token = await authService.CreateTokenAsync(user, ct);
                var response = new Response { Token = token, User = user };
                await SendAsync(response, cancellation: ct);
            },
            error =>
            {
                AddError(error.Message);
                return SendErrorsAsync(cancellation: ct);
            });
    }
}