namespace RedditClone.Application.Comment.Commands.UpdateComment;

using FluentValidation;
public partial class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(c => c.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(c => c.CommentId)
        .NotNull()
            .WithMessage("An error occurred invalid comment");

        RuleFor(c => c.Content)
            .NotNull()
                .WithMessage("Content cannot be null")
            .NotEmpty()
                .WithMessage("Content cannot be empty");
    }
}