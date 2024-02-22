namespace RedditClone.API.Mappers;

using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Community.Commands.DeleteComment;
using RedditClone.Contracts.Comment;
using RedditClone.Contracts.Comment.CreateComment.Models;
using RedditClone.Contracts.Comment.DeleteComment;
using RedditClone.Contracts.Comment.UpdateComment;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class CommentMappers
{
    public static CreateCommentCommand MapCreateCommentRequest(
            CreateCommentRequest request,
            Guid postId)
    {
        List<Votes> votes = new();
        List<Replies> replies = new();
        return new CreateCommentCommand(
            new UserId(request.UserId),
            new PostId(postId),
            request.Content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            votes,
            replies
        );
    }

    public static CreateCommentResponse MapCreateCommentResponse(
        CreateCommentResult result)
    {
        var comment = result.Comment;
        List<CreateCommentVotes> votes = new();
        List<CreateCommentReplies> replies = new();
        return new CreateCommentResponse(
            comment.Id.Value.ToString(),
            comment.UserId.Value.ToString(),
            comment.PostId.Value.ToString(),
            comment.Content,
            comment.CreatedAt,
            comment.UpdatedAt,
            votes,
            replies
        );
    }

    public static UpdateCommentCommand MapUpdateCommentRequest(
            UpdateCommentRequest request,
            Guid commentId)
    {
        return new UpdateCommentCommand(
            new CommentId(commentId),
            new UserId(request.UserId),
            request.Content
        );
    }

    public static DeleteCommentCommand MapDeleteCommentRequest(Guid commentId, DeleteCommentRequest request)
    {
        return new DeleteCommentCommand(
            new CommentId(commentId),
            new UserId(request.UserId)
        );
    }
}