namespace RedditClone.Contracts.RemoveUserCommunities;

public record RemoveUserCommunitiesRequest(
    Guid UserId,
    Guid CommunityId
);