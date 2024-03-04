namespace RedditClone.Application.Common.Interfaces.Persistence;

using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.UserCommunitiesAggregate;

public interface IUserCommunitiesRepository
{
    void Add(UserCommunities userCommunities);
    void Remove(UserId userId, CommunityId communityId);
}