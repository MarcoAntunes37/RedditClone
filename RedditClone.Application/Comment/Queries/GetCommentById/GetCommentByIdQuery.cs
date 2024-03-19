namespace RedditClone.Application.Comment.Queries.GetCommentById;

using MediatR;
using RedditClone.Application.Comment.Results.GetCommentByIdResult;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public record GetCommentByIdQuery(
    CommentId CommentId
): IRequest<GetCommentByIdResult>;