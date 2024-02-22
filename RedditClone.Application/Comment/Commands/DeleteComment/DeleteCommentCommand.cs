namespace RedditClone.Application.Community.Commands.DeleteComment;

using MediatR;
using RedditClone.Application.Community.Results.DeleteCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record DeleteCommentCommand(
    CommentId CommentId,
    UserId UserId
) : IRequest<DeleteCommentResult>;