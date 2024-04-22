namespace RedditClone.Application.Comment.Commands.UpdateComment;

using ErrorOr;
using MediatR;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.Comment.Results.UpdateCommentResult;

public record UpdateCommentCommand(
    CommentId CommentId,
    UserId UserId,
    string Content
) : IRequest<ErrorOr<UpdateCommentResult>>;