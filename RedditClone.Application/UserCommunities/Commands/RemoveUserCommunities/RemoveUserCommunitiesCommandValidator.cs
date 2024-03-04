namespace RedditClone.Application.UserCommunities.Commands.RemoveUserCommunities;

using FluentValidation;

public partial class RemoveUserCommunitiesCommandValidator : AbstractValidator<RemoveUserCommunitiesCommand>
{
    public RemoveUserCommunitiesCommandValidator()
    {
        RuleFor(uc => uc.UserId)
            .NotNull()
                .WithMessage("User is invalid");

        RuleFor(uc => uc.CommunityId)
            .NotNull()
                .WithMessage("Community is invalid");
    }
}