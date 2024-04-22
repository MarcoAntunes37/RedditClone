namespace RedditClone.Application.UserCommunities.Commands.UserCommunityRoleUpdate;

using FluentValidation;

public partial class UserCommunityRoleUpdateCommandValidator : AbstractValidator<UserCommunityRoleUpdateCommand>
{
    public UserCommunityRoleUpdateCommandValidator()
    {
        RuleFor(uc => uc.UserId)
            .NotNull()
                .WithMessage("User is invalid");

        RuleFor(uc => uc.CommunityId)
            .NotNull()
                .WithMessage("Community is invalid");

        RuleFor(uc => uc.Role)
            .NotNull()
                .WithMessage("Role is invalid");
    }
}