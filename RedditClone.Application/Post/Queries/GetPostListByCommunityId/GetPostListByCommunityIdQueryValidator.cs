namespace RedditClone.Application.Post.Queries.GetCommentListByCommunityId;

using FluentValidation;
using RedditClone.Application.Community.Queries.GetPostListByCommunityId;

public partial class GetPostListByCommunityIdQueryValidator : AbstractValidator<GetPostListByCommunityIdQuery>
{
    public GetPostListByCommunityIdQueryValidator()
    {
        RuleFor(c => c.CommunityId)
            .NotNull()
                .WithMessage("Invalid Community");
    }
}