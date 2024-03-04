namespace RedditClone.Application.UserCommunities.Commands.RemoveUserCommunities;

using FluentValidation;
using MediatR;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.AddUserCommunitiesResults;

public class RemoveUserCommunitiesCommandHandler
    : IRequestHandler<RemoveUserCommunitiesCommand, RemoveUserCommunitiesResult>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository;
    private readonly IValidator<RemoveUserCommunitiesCommand> _validator;

    public RemoveUserCommunitiesCommandHandler(
        IUserCommunitiesRepository userCommunityRepository,
        IValidator<RemoveUserCommunitiesCommand> validator)
    {
        _userCommunityRepository = userCommunityRepository;
        _validator = validator;
    }

    public async Task<RemoveUserCommunitiesResult> Handle(RemoveUserCommunitiesCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        _validator.ValidateAndThrow(command);

        _userCommunityRepository.Remove(command.UserId, command.CommunityId);

        return new RemoveUserCommunitiesResult(
            "User left community successfully"
        );
    }
}