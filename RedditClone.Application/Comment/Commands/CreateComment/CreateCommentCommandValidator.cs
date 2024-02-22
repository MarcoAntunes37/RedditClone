namespace RedditClone.Application.Comment.Commands.CreateComment;

using FluentValidation;

public partial class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Content)
            .NotEmpty()
                .WithMessage("Content cannot be empty")
            .NotNull()
                .WithMessage("Content cannot be null")
            .Length(2, 255)
                .WithMessage("Content must have at least 2 and at maximum 255 characters");
    }
}