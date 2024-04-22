namespace RedditClone.Application.CommentReplies.Commands.UpdateCommentReply;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.CommentReplies.Results.UpdateCommentReplyResults;

public record UpdateCommentReplyCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    UserId UserId,
    string Content)
: IRequest<ErrorOr<UpdateCommentReplyResult>>;