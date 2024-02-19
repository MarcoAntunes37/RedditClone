namespace RedditClone.Application.Community.Commands.TransferCommunity;

using FluentValidation;

public partial class TransferCommunityCommandValidator : AbstractValidator<TransferCommunityCommand>
{
    public TransferCommunityCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
                .WithMessage("Invalid user");

        RuleFor(c => c.NewUserId)
            .NotNull()
                .WithMessage("Invalid user");

        RuleFor(c => c.CommunityId)
            .NotNull()
                .WithMessage("Invalid community");
    }
}