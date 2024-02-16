namespace RedditClone.Application.Community.Commands.Delete;

using MediatR;
using RedditClone.Application.Community.Results.DeleteCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public record DeleteCommentCommand(
    CommentId CommentId,
    UserId UserId
) : IRequest<DeleteCommentResult>;