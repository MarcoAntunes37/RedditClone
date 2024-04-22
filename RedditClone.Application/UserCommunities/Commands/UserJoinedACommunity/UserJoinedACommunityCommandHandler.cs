namespace RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

public class UserJoinACommunityCommandHandler(
    IUserCommunitiesRepository userCommunityRepository)
        : IRequestHandler<UserJoinACommunityCommand, ErrorOr<UserJoinACommunityResult>>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository = userCommunityRepository;

    public async Task<ErrorOr<UserJoinACommunityResult>> Handle(
        UserJoinACommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UserJoinACommunityCommand}",
            "Trying to add User: {@UserId} to community: {@CommunityId}",
            command,
            command.UserId,
            command.CommunityId);

        if(_userCommunityRepository.GetUserCommunities(command.UserId, command.CommunityId) == null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        var userCommunities = UserCommunities.Create(
            command.UserId,
            command.CommunityId,
            command.Role
        );

        _userCommunityRepository.Add(userCommunities);

        UserJoinACommunityResult result = new("User joined in community successfully");

        Log.Information(
            "{@UserJoinACommunityResult}, UserId: {@UserId}, CommunityId: {@CommunityId}",
            result,
            command.UserId,
            command.CommunityId,
            command.Role);

        return result;
    }
}