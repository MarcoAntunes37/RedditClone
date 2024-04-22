namespace RedditClone.Application.CommentReplies.Commands.DeleteCommentReply;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Results.DeleteCommentReplyResults;

public record DeleteCommentReplyCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    UserId UserId
) : IRequest<ErrorOr<DeleteCommentReplyResult>>;