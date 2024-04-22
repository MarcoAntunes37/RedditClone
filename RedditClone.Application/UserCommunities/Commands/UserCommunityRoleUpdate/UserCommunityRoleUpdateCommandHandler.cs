namespace RedditClone.Application.UserCommunities.Commands.UserJoinACommunity;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.UserCommunities.Commands.UserCommunityRoleUpdate;
using RedditClone.Application.UserCommunities.Results.UserCommunityRoleUpdateResult;

public class UserCommunityRoleUpdateCommandHandler(
    IUserCommunitiesRepository userCommunityRepository)
        : IRequestHandler<UserCommunityRoleUpdateCommand, ErrorOr<UserCommunityRoleUpdateResult>>
{
    private readonly IUserCommunitiesRepository _userCommunityRepository = userCommunityRepository;


    public async Task<ErrorOr<UserCommunityRoleUpdateResult>> Handle(
        UserCommunityRoleUpdateCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@UserCommunityRoleUpdateCommand}",
            "Trying to add User: {@UserId} to community: {@CommunityId}",
            command,
            command.UserId,
            command.CommunityId);

        var userCommunities = _userCommunityRepository.GetUserCommunities(command.UserId, command.CommunityId);

        var admin = _userCommunityRepository.GetUserCommunitiesAdmin(command.CommunityId);

        if (userCommunities is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error(
                "{@Code}, {@Descriptor}",
                error.Code,
                error.Description);

            return error;
        }

        if (command.Role == Role.Admin)
        {
            _userCommunityRepository.UpdateRole(
                admin.UserId, command.CommunityId, Role.Member);

            _userCommunityRepository.UpdateRole(
                command.UserId, command.CommunityId, command.Role);
        }
        else
        {
            _userCommunityRepository.UpdateRole(
                command.UserId, command.CommunityId, command.Role);
        }

        UserCommunityRoleUpdateResult result = new("User role updated successfully");

        Log.Information(
            "{@UserCommunityRoleUpdateResult}, UserId: {@UserId}, CommunityId: {@CommunityId}",
            result,
            command.UserId,
            command.CommunityId,
            command.Role);

        return result;
    }
}