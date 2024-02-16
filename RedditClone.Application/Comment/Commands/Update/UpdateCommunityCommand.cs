namespace RedditClone.Application.Community.Commands.Update;

using MediatR;
using RedditClone.Application.Community.Results.UpdateCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public record UpdateCommentCommand(
    CommentId CommentId,
    UserId UserId,
    string Content
) : IRequest<UpdateCommentResult>;