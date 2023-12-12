using FastEndpoints;
using FluentValidation;
using Mangifier.Api.Shared.Users.UpdateImage;

namespace Mangifier.Api.Services.Users.UpdateImage;

public sealed class Validator : Validator<Request>
{
    public Validator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Required field");
    }
}