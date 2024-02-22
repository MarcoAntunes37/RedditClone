namespace RedditClone.Application.Comment.Commands.UpdateComment;

using MediatR;
using RedditClone.Application.Community.Results.UpdateCommentResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public record UpdateCommentCommand(
    CommentId CommentId,
    UserId UserId,
    string Content
) : IRequest<UpdateCommentResult>;