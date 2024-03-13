namespace RedditClone.Application.Community.Commands.GetCommunityById;

using FluentValidation;
using RedditClone.Application.Community.Queries.GetCommunitiesById;

public partial class GetCommunityByIdQueryValidator : AbstractValidator<GetCommunityByIdQuery>
{
    public GetCommunityByIdQueryValidator()
    {
        RuleFor(c => c.CommunityId)
            .NotNull()
                .WithMessage("Invalid community");
    }
}