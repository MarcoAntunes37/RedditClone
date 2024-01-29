using RedditClone.Domain.Common.Models;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Domain.UserAggregate.Entities;

public sealed class UserCommunities
: Entity<CommunityId>
{
    public string Name {get;}
    private UserCommunities(CommunityId communityId, string name)
    : base(communityId)
    {
        Name = name;
    }

    public static UserCommunities Create(
        CommunityId communityId,
        string name
    )
    {
        return new(communityId, name);
    }

}