namespace RedditClone.API.Endpoints.CommentReplies.CreateReply;

public record CreateReplyRequest(
        Guid UserId,
        Guid CommunityId,
        string Content);