namespace RedditClone.Application.Community.Commands.DeleteCommunity;

using FluentValidation;

public partial class DeleteCommunityCommandValidator : AbstractValidator<DeleteCommunityCommand>
{
    public DeleteCommunityCommandValidator()
    {
        RuleFor(u => u.UserId)
            .NotNull()
                .WithMessage("An error occurred invalid user");

        RuleFor(u => u.CommunityId)
        .NotNull()
            .WithMessage("An error occurred invalid community");
    }
}