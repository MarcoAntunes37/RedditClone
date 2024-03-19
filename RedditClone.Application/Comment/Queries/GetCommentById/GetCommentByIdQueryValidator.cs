namespace RedditClone.Application.Comment.Queries.GetCommentById;

using FluentValidation;

public partial class GetCommentByIdQueryValidator : AbstractValidator<GetCommentByIdQuery>
{
    public GetCommentByIdQueryValidator()
    {
        RuleFor(c => c.CommentId)
            .NotNull()
                .WithMessage("Invalid Comment");
    }
}