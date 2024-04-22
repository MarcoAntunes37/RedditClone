namespace RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;

using FluentValidation;

public partial class UserLeftACommunityCommandValidator : AbstractValidator<UserLeftACommunityCommand>
{
    public UserLeftACommunityCommandValidator()
    {
        RuleFor(uc => uc.UserId)
            .NotNull()
                .WithMessage("User is invalid");

        RuleFor(uc => uc.CommunityId)
            .NotNull()
                .WithMessage("Community is invalid");
    }
}