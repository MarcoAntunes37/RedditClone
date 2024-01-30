namespace RedditClone.Contracts.Post;

public record CreatePostRequest(
    string Title,
    string Content
);
