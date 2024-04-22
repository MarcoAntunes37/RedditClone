using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.API.Endpoints.Comment.CreateComment;

public record CreateCommentRequest(
    Guid UserId,
    Guid CommunityId,
    Guid PostId,
    string Content
);
