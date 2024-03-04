namespace RedditClone.Application.UserCommunities.Commands.CreatePost;

using RedditClone.Application.UserCommunities.Commands.AddUserCommunities;
using FluentValidation;

public partial class AddUserCommunitiesCommandValidator : AbstractValidator<AddUserCommunitiesCommand>
{
    public AddUserCommunitiesCommandValidator()
    {
        RuleFor(uc => uc.UserId)
            .NotNull()
                .WithMessage("User is invalid");

        RuleFor(uc => uc.CommunityId)
            .NotNull()
                .WithMessage("Community is invalid");
    }
}