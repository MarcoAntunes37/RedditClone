namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using FluentValidation;
using RedditClone.Application.Comment.Queries.GetCommentsListByPostId;

public partial class GetCommentsListByPostIdQueryValidator : AbstractValidator<GetCommentsListByPostIdQuery>
{
    public GetCommentsListByPostIdQueryValidator()
    {
        RuleFor(c => c.PostId)
            .NotNull()
                .WithMessage("Invalid Post");
    }
}