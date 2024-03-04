namespace RedditClone.Application.Comment.Commands.UpdateReplyOnComment;

using MediatR;
using RedditClone.Application.Comment.Results.UpdateReplyOnCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UpdateReplyOnCommentCommand(
    CommentId CommentId,
    ReplyId ReplyId,
    UserId UserId,
    string Content)
: IRequest<UpdateReplyOnCommentResult>;