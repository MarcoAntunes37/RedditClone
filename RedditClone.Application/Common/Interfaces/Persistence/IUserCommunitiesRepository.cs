namespace RedditClone.Application.Common.Interfaces.Persistence;

using ErrorOr;
using RedditClone.Domain.UserCommunitiesAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate.Enum;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public interface IUserCommunitiesRepository
{
    UserCommunities GetUserCommunities(UserId userId, CommunityId communityId);
    UserCommunities GetUserCommunitiesAdmin(CommunityId communityId);
    void Add(UserCommunities userCommunities);
    ErrorOr<bool> UpdateRole(UserId userId, CommunityId communityId, Role role);
    ErrorOr<bool> Remove(UserId userId, CommunityId communityId);
}