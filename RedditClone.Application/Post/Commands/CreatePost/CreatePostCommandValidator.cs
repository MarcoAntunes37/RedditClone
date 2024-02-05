using FluentValidation;

namespace RedditClone.Application.Post.Commands.CreatePost;

public partial class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(p => p.Title)
            .NotEmpty()
                .WithMessage("Title cannot be empty")
            .NotNull()
                .WithMessage("Title cannot be null")
            .Length(2, 100)
                .WithMessage("Title must have at least 2 and at maximum 100 characters");

        RuleFor(u => u.Content)
            .NotEmpty()
                .WithMessage("Content cannot be empty")
            .NotNull()
                .WithMessage("Content cannot be null")
            .Length(2, 200)
                .WithMessage("Content must have at least 2 and at maximum 200 characters");
    }
}