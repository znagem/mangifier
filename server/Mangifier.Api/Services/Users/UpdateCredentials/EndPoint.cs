using FastEndpoints;
using Mangifier.Api.DataAccess.Users.UpdateCredentials;
using Mangifier.Api.Shared.Users;
using Mangifier.Api.Shared.Users.UpdateCredentials;

namespace Mangifier.Api.Services.Users.UpdateCredentials;

public sealed class EndPoint(IRepositoryFactory factory) : Endpoint<Request, UserDto>
{
    public override void Configure()
    {
        Put("users/credentials");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var modifiedBy = User.Claims.First(i => i.Type == "id").Value;

        if (req.Id != modifiedBy)
            ThrowError("You are not allowed to update this user's credentials", StatusCodes.Status401Unauthorized);

        var repository = factory.Create<Repository>();
        var result = await repository.ExecuteAsync(req, modifiedBy, ct);

        await result.Match<Task>(
            async user =>
            {
                await SendAsync(user, cancellation: ct);
            },
            error =>
            {
                AddError(error.Message);
                return SendErrorsAsync(cancellation: ct);
            });
    }
}