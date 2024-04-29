namespace RedditClone.Application.UserCommunities.Queries.GetUserListByCommunityId;

using FluentValidation;

public partial class GetUserListByCommunityIdQueryValidator : AbstractValidator<GetUserListByCommunityIdQuery>
{
    public GetUserListByCommunityIdQueryValidator()
    {
        RuleFor(x => x.CommunityId)
            .NotNull()
            .WithMessage("Community is invalid");
    }
}