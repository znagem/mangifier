using FastEndpoints;
using FluentValidation;
using Mangifier.Api.Shared.Users.Login;

namespace Mangifier.Api.Services.Users.Login;

public sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required");
    }
}