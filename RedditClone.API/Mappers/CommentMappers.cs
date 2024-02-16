
using RedditClone.Application.Comment.Commands.CreateCommentCommand;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Community.Commands.Delete;
using RedditClone.Application.Community.Commands.Update;
using RedditClone.Contracts.Comment;
using RedditClone.Contracts.Comment.CreateComment.Models;
using RedditClone.Contracts.Community.DeleteComment;
using RedditClone.Contracts.Community.UpdateComment;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.API.Mappers;

public class CommentMappers
{
    public static CreateCommentCommand MapCreateCommentRequest(
            CreateCommentRequest request,
            Guid postId)
    {
        List<Votes> votes = new();
        List<Replies> replies = new();
        return new CreateCommentCommand(
            UserId.Create(request.UserId),
            PostId.Create(postId),
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
            CommentId.Create(commentId),
            UserId.Create(request.UserId),
            request.Content
        );
    }

    public static DeleteCommentCommand MapDeleteCommentRequest(Guid commentId, DeleteCommentRequest request)
    {
        return new DeleteCommentCommand(
            CommentId.Create(commentId),
            UserId.Create(request.UserId)
        );
    }
}