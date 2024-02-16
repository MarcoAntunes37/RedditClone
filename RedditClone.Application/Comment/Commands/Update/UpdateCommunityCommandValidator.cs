namespace RedditClone.Application.Comment.Commands.Update;

using FluentValidation;
using RedditClone.Application.Community.Commands.Update;

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