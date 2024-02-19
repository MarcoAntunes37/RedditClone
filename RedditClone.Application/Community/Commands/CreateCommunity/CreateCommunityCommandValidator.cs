namespace RedditClone.Application.Community.Commands.CreateCommunity;

using FluentValidation;

public partial class CreateCommunityCommandValidator : AbstractValidator<CreateCommunityCommand>
{
    public CreateCommunityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
                .WithMessage("Name cannot be empty")
            .NotNull()
                .WithMessage("Name cannot be null")
            .Length(2, 100)
                .WithMessage("Name must have at least 2 and at maximum 100 characters");

        RuleFor(u => u.Description)
            .NotEmpty()
                .WithMessage("Description cannot be empty")
            .NotNull()
                .WithMessage("Description cannot be null")
            .Length(2, 200)
                .WithMessage("Description must have at least 2 and at maximum 200 characters");

        RuleFor(u => u.Topic)
            .NotEmpty()
                .WithMessage("Topic cannot be empty")
            .NotNull()
                .WithMessage("Topic cannot be null")
                .Length(2, 100)
                .WithMessage("Topic must have at least 2 and at maximum 100 characters");
    }
}