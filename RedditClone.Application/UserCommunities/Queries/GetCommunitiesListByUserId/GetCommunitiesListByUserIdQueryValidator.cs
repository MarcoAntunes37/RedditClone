using FluentValidation;

namespace RedditClone.Application.UserCommunities.Queries.GetCommunitiesListByUserId;

public class GetCommunitiesListByUserIdQueryValidator : AbstractValidator<GetCommunitiesListByUserIdQuery>
{
    public GetCommunitiesListByUserIdQueryValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("User is invalid");
    }
}