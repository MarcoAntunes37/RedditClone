using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Domain.UserCommunitiesAggregate;

public sealed class UserCommunities
{
    public UserId UserId { get; private set; }
    public CommunityId CommunityId { get; private set; }

#pragma warning disable CS8618
    private UserCommunities() { }
#pragma warning restore

    private UserCommunities(UserId userId, CommunityId communityId)
    {
        UserId = userId;
        CommunityId = communityId;
    }

    public static UserCommunities Create(UserId userId, CommunityId communityId)
    {
        return new(
            userId,
            communityId
        );
    }
}