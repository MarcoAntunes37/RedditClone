namespace RedditClone.Contracts.Community.TransferCommunityOwnership;

public record TransferCommunityOwnershipRequest(
    Guid OwnerId,
    Guid NewOwnerId
);
