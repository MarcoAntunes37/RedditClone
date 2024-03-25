namespace RedditClone.Application.Comment.Queries.GetCommentsByPostId;

using MediatR;
using RedditClone.Application.Comment.Results.GetCommentsByPostIdResult;
using RedditClone.Domain.PostAggregate.ValueObjects;

public record GetCommentsByPostIdQuery(
    PostId PostId
): IRequest<GetCommentsByPostIdResult>;