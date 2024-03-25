namespace RedditClone.Application.Post.Results.UpdatePostResult;

using RedditClone.Domain.PostAggregate;

public record UpdatePostResult(
    string Message,
    Post Post
);