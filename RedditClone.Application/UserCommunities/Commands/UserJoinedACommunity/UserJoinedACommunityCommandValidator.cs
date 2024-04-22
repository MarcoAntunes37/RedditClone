namespace RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;

using FluentValidation;

public partial class UserJoinACommunityCommandValidator : AbstractValidator<UserJoinACommunityCommand>
{
    public UserJoinACommunityCommandValidator()
    {
        RuleFor(uc => uc.UserId)
            .NotNull()
                .WithMessage("User is invalid");

        RuleFor(uc => uc.CommunityId)
            .NotNull()
                .WithMessage("Community is invalid");
    }
}