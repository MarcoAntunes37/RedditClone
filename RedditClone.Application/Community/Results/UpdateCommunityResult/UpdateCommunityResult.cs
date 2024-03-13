namespace RedditClone.Application.Community.Results.UpdateCommunityResult;

using RedditClone.Domain.CommunityAggregate;

public record UpdateCommunityResult(
    string Message,
    Community Community
);