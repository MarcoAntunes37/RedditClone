namespace RedditClone.API.Endpoints.CommentReplies.UpdateReply;

public record UpdateReplyRequest(
    Guid UserId,
    string Content);