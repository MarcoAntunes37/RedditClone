using RedditClone.Domain.PostAggregate;

namespace RedditClone.Application.Post.Results;

public record CreatePostResult(
    PostAggregate Post
);