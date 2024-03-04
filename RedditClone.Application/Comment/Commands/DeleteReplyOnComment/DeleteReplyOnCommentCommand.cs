namespace RedditClone.Application.Community.Commands.DeleteReplyOnComment;

using MediatR;
using RedditClone.Application.Community.Results.DeleteReplyOnCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteReplyOnCommentCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    UserId UserId
) : IRequest<DeleteReplyOnCommentResult>;