namespace RedditClone.Application.UserCommunities.Queries.GetUserCommunity;

using FluentValidation;

public partial class GetUserCommunityQueryValidator : AbstractValidator<GetUserCommunityQuery>
{
    public GetUserCommunityQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("User is invalid");

        RuleFor(x => x.CommunityId)
            .NotNull()
            .WithMessage("Community is invalid");
    }
}