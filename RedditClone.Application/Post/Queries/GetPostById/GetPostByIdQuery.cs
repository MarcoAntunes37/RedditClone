namespace RedditClone.Application.Post.Queries.GetPostById;

using MediatR;
using RedditClone.Application.Post.Results.GetPostByIdResult;
using RedditClone.Domain.PostAggregate.ValueObjects;

public record GetPostByIdQuery(
    PostId PostId
): IRequest<GetPostByIdResult>;