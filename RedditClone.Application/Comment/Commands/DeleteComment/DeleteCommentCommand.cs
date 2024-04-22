
using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.Comment.Results.DeleteCommentResult;

namespace RedditClone.Application.Comment.Commands.DeleteComment;
public record DeleteCommentCommand(
    CommentId CommentId,
    UserId UserId
) : IRequest<ErrorOr<DeleteCommentResult>>;