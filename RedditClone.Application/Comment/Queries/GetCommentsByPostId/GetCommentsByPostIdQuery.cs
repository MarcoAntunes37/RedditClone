namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.Comment.Results.GetCommentsByPostIdResult;

public record GetCommentsByPostIdQuery(
    PostId PostId,
    int Page,
    int PageSize
): IRequest<GetCommentsByPostIdResult>;