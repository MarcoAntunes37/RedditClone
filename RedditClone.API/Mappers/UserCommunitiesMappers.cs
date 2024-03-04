namespace RedditClone.API.Mappers;

using RedditClone.Application.UserCommunities.Commands.AddUserCommunities;
using RedditClone.Application.UserCommunities.Commands.RemoveUserCommunities;
using RedditClone.Contracts.AddUserCommunities;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public static class UserCommunitiesMappers
{
    public static AddUserCommunitiesCommand MapAddUserCommunitiesRequest(AddUserCommunitiesRequest request)
    {
        return new AddUserCommunitiesCommand(
            new CommunityId(request.CommunityId),
            new UserId(request.UserId)
        );
    }

    public static RemoveUserCommunitiesCommand MapRemoveUserCommunitiesRequest(Guid userId, Guid communityId)
    {
        return new RemoveUserCommunitiesCommand(
            new CommunityId(communityId),
            new UserId(userId)
        );
    }
}