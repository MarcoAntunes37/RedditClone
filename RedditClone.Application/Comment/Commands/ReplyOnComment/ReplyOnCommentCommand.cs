namespace RedditClone.Application.Comment.Commands.ReplyOnComment;

using MediatR;
using RedditClone.Application.Comment.Results.ReplyOnCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record ReplyOnCommentCommand(
    UserId UserId,
    CommentId CommentId,
    string Content)
: IRequest<ReplyOnCommentResult>;