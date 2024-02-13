using FluentValidation;
using RedditClone.Application.User.Commands.UpdateProfile;

namespace RedditClone.Application.User.Commands.Update;

public partial class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(u => u.Firstname)
            .NotEmpty()
                .WithMessage("Firstname cannot be empty")
            .NotNull()
                .WithMessage("Firstname cannot be null")
            .Length(2, 100)
                .WithMessage("Firstname must have at least 2 and at maximum 100 characters");

        RuleFor(u => u.Lastname)
            .NotEmpty()
                .WithMessage("Lastname cannot be empty")
            .NotNull()
                .WithMessage("Lastname cannot be null")
            .Length(2, 100)
                .WithMessage("Lastname must have at least 2 and at maximum 100 characters");

        RuleFor(u => u.Email)
            .NotEmpty()
                .WithMessage("Email cannot be empty")
            .NotNull()
                .WithMessage("Email cannot be null")
            .EmailAddress()
                .WithMessage("Email must be a valid email address");
    }
}