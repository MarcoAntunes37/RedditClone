namespace RedditClone.Application.Comment.Queries.GetCommentById;

using MediatR;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Application.Comment.Results.GetCommentByIdResult;

public record GetCommentByIdQuery(
    CommentId CommentId
): IRequest<GetCommentByIdResult>;