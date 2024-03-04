namespace RedditClone.API.Mappers;

using RedditClone.Application.Comment.Commands.UpdateComment;
using RedditClone.Application.Comment.Commands.CreateComment;
using RedditClone.Application.Comment.Results.CreateCommentResult;
using RedditClone.Application.Community.Commands.DeleteComment;
using RedditClone.Contracts.Comment.CreateComment.Models;
using RedditClone.Contracts.Comment.DeleteComment;
using RedditClone.Contracts.Comment.UpdateComment;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Contracts.Comment.CreateComment;
using RedditClone.Application.Comment.Commands.VoteOnComment;
using RedditClone.Contracts.Comment.VoteOnComment;
using RedditClone.Application.Comment.Commands.UpdateVoteOnComment;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Application.Community.Commands.DeleteVoteOnComment;
using RedditClone.Application.Comment.Commands.ReplyOnComment;
using RedditClone.Contracts.Comment.ReplyOnComment;
using RedditClone.Application.Comment.Commands.UpdateReplyOnComment;
using RedditClone.Contracts.Comment.UpdateReplyOnComment;
using RedditClone.Application.Community.Commands.DeleteReplyOnComment;
using RedditClone.Contracts.Comment.DeleteReplyOnComment;
using RedditClone.Contracts.Comment.UpdateVoteOnComment;
using RedditClone.Contracts.Comment.DeleteVoteOnComment;
using RedditClone.Application.Comment.Commands.VoteOnReply;
using RedditClone.Contracts.Comment.VoteOnReply;
using RedditClone.Contracts.Comment.UpdateVoteOnReply;
using RedditClone.Application.Comment.Commands.UpdateVoteOnReply;
using RedditClone.Contracts.Comment.DeleteVoteOnReply;
using RedditClone.Application.Community.Commands.DeleteVoteOnReply;

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

    public static DeleteCommentCommand MapDeleteCommentRequest(
        Guid commentId,
        DeleteCommentRequest request)
    {
        return new DeleteCommentCommand(
            new CommentId(commentId),
            new UserId(request.UserId)
        );
    }

    public static VoteOnCommentCommand MapVoteOnCommentRequest(
        Guid commentId,
        VoteOnCommentRequest request)
    {
        return new VoteOnCommentCommand(
            new CommentId(commentId),
            new UserId(request.UserId),
            request.IsVoted
        );
    }

    public static UpdateVoteOnCommentCommand MapUpdateVoteOnCommentRequest(
            UpdateVoteOnCommentRequest request,
            Guid commentId,
            Guid voteId)
    {
        return new UpdateVoteOnCommentCommand(
            new CommentId(commentId),
            new VoteId(voteId),
            new UserId(request.UserId),
            request.IsVoted
        );
    }

    public static DeleteVoteOnCommentCommand MapDeleteVoteOnCommentRequest(
        Guid commentId,
        Guid voteId,
        DeleteVoteOnCommentRequest request)
    {
        return new DeleteVoteOnCommentCommand(
            new CommentId(commentId),
            new VoteId(voteId),
            new UserId(request.UserId)
        );
    }

    public static ReplyOnCommentCommand MapReplyOnCommentRequest(
        Guid commentId,
        ReplyOnCommentRequest request)
    {
        return new ReplyOnCommentCommand(
            new UserId(request.UserId),
            new CommentId(commentId),
            request.Content
        );
    }

    public static UpdateReplyOnCommentCommand MapUpdateReplyOnCommentRequest(
            UpdateReplyOnCommentRequest request,
            Guid commentId,
            Guid replyId)
    {
        return new UpdateReplyOnCommentCommand(
            new CommentId(commentId),
            new ReplyId(replyId),
            new UserId(request.UserId),
            request.Content
        );
    }

    public static DeleteReplyOnCommentCommand MapDeleteReplyOnCommentRequest(
        DeleteReplyOnCommentRequest request,
        Guid commentId,
        Guid replyId)
    {
        return new DeleteReplyOnCommentCommand(
            new CommentId(commentId),
            new ReplyId(replyId),
            new UserId(request.UserId)
        );
    }

    public static VoteOnReplyCommand MapVoteOnReplyRequest(
        VoteOnReplyRequest request,
        Guid commentId,
        Guid replyId)
    {
        return new VoteOnReplyCommand(
            new CommentId(commentId),
            new ReplyId(replyId),
            new UserId(request.UserId),
            request.IsVoted
        );
    }

    public static UpdateVoteOnReplyCommand MapUpdateVoteOnReplyRequest(
        UpdateVoteOnReplyRequest request,
        Guid replyId,
        Guid voteId,
        Guid commentId)
    {
        return new UpdateVoteOnReplyCommand(
            new CommentId(commentId),
            new ReplyId(replyId),
            new VoteId(voteId),
            new UserId(request.UserId),
            request.IsVoted
        );
    }

    public static DeleteVoteOnReplyCommand MapDeleteVoteOnReplyRequest(
        DeleteVoteOnReplyRequest request,
        Guid commentId,
        Guid replyId,
        Guid voteId)
    {
        return new DeleteVoteOnReplyCommand(
            new CommentId(commentId),
            new ReplyId(replyId),
            new VoteId(voteId),
            new UserId(request.UserId)
        );
    }
}