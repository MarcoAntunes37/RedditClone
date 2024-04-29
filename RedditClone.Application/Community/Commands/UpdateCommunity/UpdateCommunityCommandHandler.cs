namespace RedditClone.Application.Community.Commands.UpdateCommunity;

using ErrorOr;
using MediatR;
using Serilog;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Community.Results.UpdateCommunityResult;

public class UpdateCommunityCommandHandler(
    ICommunityRepository communityRepository,
    IUserCommunitiesRepository userCommunitiesRepository) :
    IRequestHandler<UpdateCommunityCommand, ErrorOr<UpdateCommunityResult>>
{
    private readonly ICommunityRepository _communityRepository = communityRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<UpdateCommunityResult>> Handle(
        UpdateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "Trying to update Community: {@CommunityId} with Owner: {@UserId}");

        if (!_communityRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var admin = _userCommunitiesRepository.GetUserCommunitiesAdmin(command.CommunityId);

        if (admin.UserId != command.UserId)
        {
            Error error = Errors.UserCommunities.UserIsNotCommunityAdmin;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        _communityRepository.UpdateCommunityById(command.CommunityId, command.UserId, command.Name, command.Description, command.Topic);

        var community = _communityRepository.GetCommunityById(command.CommunityId).Value;

        UpdateCommunityResult result = new("Community successfully updated.", community);

        Log.Information(
            "{@UpdateCommunityResult}",
            result);

        return result;
    }
}