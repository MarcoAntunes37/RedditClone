namespace RedditClone.Application.Post.Results.GetPostByIdResult;

using RedditClone.Domain.PostAggregate;

public record GetPostByIdResult(
    Post Post
);