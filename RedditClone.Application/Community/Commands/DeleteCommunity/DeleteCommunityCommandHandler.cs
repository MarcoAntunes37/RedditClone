namespace RedditClone.Application.Community.Commands.DeleteCommunity;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Community.Results.DeleteCommunityResult;

public class DeleteCommunityCommandHandler(
    ICommunityRepository communityRepository,
    IUserCommunitiesRepository userCommunitiesRepository)
    : IRequestHandler<DeleteCommunityCommand, ErrorOr<DeleteCommunityResult>>
{
    private readonly ICommunityRepository _communityRepository = communityRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<DeleteCommunityResult>> Handle(
        DeleteCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@DeleteCommunityCommand}",
            "Trying to delete Community: {@CommunityId} with Owner: {@UserId}",
            command,
            command.CommunityId,
            command.UserId);

        var userCommunities = _userCommunitiesRepository.GetUserCommunities(command.UserId, command.CommunityId);

        if (userCommunities is null)
        {
            Error error = Errors.UserCommunities.UserNotInCommunity;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (userCommunities.Role != Role.Admin)
        {
            Error error = Errors.UserCommunities.UserIsNotCommunityAdmin;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _communityRepository.DeleteCommunityById(command.CommunityId, command.UserId);

        DeleteCommunityResult result = new("Community successfully Deleted.");

        Log.Information(
            "{@DeleteCommunityResult}, {@CommunityId}",
            result,
            command.CommunityId);

        return result;
    }
}