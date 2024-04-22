namespace RedditClone.Application.Post.Queries.GetPostById;

using ErrorOr;
using MediatR;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Application.Post.Results.GetPostByIdResult;

public record GetPostByIdQuery(
    PostId PostId
): IRequest<ErrorOr<GetPostByIdResult>>;