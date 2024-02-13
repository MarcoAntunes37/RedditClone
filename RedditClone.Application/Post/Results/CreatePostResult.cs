namespace RedditClone.Application.Post.Results;

using RedditClone.Domain.PostAggregate;

public record CreatePostResult(
    Post Post
);