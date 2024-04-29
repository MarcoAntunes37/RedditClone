namespace RedditClone.Application.Comment.Queries.GetCommentsListByPostId;

using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.Comment.Results.GetCommentsListByPostIdResults;

public record GetCommentsListByPostIdQuery(
    PostId PostId,
    int Page,
    int PageSize
): IRequest<GetCommentsListByPostIdResult>;