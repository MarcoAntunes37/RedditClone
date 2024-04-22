namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using FluentValidation;

public partial class GetCommentsByPostIdQueryValidator : AbstractValidator<GetCommentsByPostIdQuery>
{
    public GetCommentsByPostIdQueryValidator()
    {
        RuleFor(c => c.PostId)
            .NotNull()
                .WithMessage("Invalid Post");
    }
}