namespace RedditClone.Application.Community.Commands.CreateCommunity;

using MediatR;
using Serilog;
using ErrorOr;
using RedditClone.Domain.Common.Errors;
using RedditClone.Application.Persistence;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Application.Common.Interfaces.Persistence;
using RedditClone.Application.Community.Results.CreateCommunityResult;

public class CreateCommunityCommandHandler(
    ICommunityRepository communityRepository,
    IUserCommunitiesRepository userCommunitiesRepository) :
    IRequestHandler<CreateCommunityCommand, ErrorOr<CreateCommunityResult>>
{
    private readonly ICommunityRepository _communityRepository = communityRepository;
    private readonly IUserCommunitiesRepository _userCommunitiesRepository = userCommunitiesRepository;

    public async Task<ErrorOr<CreateCommunityResult>> Handle(
        CreateCommunityCommand command,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Log.Information(
            "{@Message}, {@CreateCommunityCommand}",
            "Trying to create Community with Admin: {@UserId}",
            command.UserId);

        if (_communityRepository.GetCommunityByName(command.Name).Value is not null)
        {
            Error error = Errors.Community.CommunityNameAlreadyExists;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        if (!_communityRepository.UserExists(command.UserId))
        {
            Error error = Errors.User.UserNotFound;

            Log.Error("{@Code}, {@Description}",
                error.Code,
                error.Description);

            return error;
        }

        var community = Community.Create(
            command.Name,
            command.Description,
            command.Topic,
            command.UserId);

        _communityRepository.Add(community);

        var userCommunities = UserCommunities.Create(
            command.UserId,
            community.Id,
            (int)Role.Admin);

        _userCommunitiesRepository.Add(userCommunities);

        CreateCommunityResult result = new(community);

        Log.Information(
            "{@Message}", "{@CreateCommunityResult}",
            "Community created successfully",
            result);

        return result;
    }
}