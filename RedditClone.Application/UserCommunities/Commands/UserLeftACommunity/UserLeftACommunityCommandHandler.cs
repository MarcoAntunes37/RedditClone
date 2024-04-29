namespace RedditClone.Application.UserCommunities.Commands.UserLeftACommunity;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Results.UserJoinACommunityResults;

public class UserLeftACommunityCommandHandler
    : IRequestHandler<UserLeftACommunityCommand, ErrorOr<UserLeftACommunityResult>>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository;

    public UserLeftACommunityCommandHandler(
        IUserCommunitiesRepository userCommunityRepository)
    {
        _userCommunityRepository = userCommunityRepository;
    }

    public async Task<ErrorOr<UserLeftACommunityResult>> Handle(
        UserLeftACommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UserLeftACommunityCommand}",
            "Trying to remove User: {@UserId} from Community: {@CommunityId}",
            command,
            command.UserId,
            command.CommunityId);

        if (!_userCommunityRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (!_userCommunityRepository.CommunityExists(command.CommunityId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        var userCommunities = _userCommunityRepository.GetUserCommunities(command.UserId, command.CommunityId);

        if (userCommunities is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (userCommunities.Role is Role.Admin)
        {
            Error error = Errors.UserCommunities.AdminCantLeaveCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }
        else
        {
            userCommunities.Delete();

            _userCommunityRepository.Remove(command.UserId, command.CommunityId);

            UserLeftACommunityResult result = new("User left community successfully");

            Log.Information(
                "{@UserLeftACommunityResult}, User: {@UserId}, Community: {@CommunityId}",
                result,
                command.UserId,
                command.CommunityId);

            return result;
        }
    }
}