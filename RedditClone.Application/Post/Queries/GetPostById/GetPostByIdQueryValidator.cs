namespace RedditClone.Application.Post.Queries.GetPostById;

using FluentValidation;

public partial class GetPostByIdQueryValidator : AbstractValidator<GetPostByIdQuery>
{
    public GetPostByIdQueryValidator()
    {
        RuleFor(c => c.PostId)
            .NotNull()
                .WithMessage("Invalid Post");
    }
}