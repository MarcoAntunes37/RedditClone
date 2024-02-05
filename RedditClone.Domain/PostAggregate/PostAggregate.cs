using RedditClone.Domain.Common.Models;
using RedditClone.Domain.PostAggregate.ValueObjects;

namespace RedditClone.Domain.PostAggregate;

public sealed class PostAggregate :
AggregateRoot<PostId>
{
    public string Title { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private PostAggregate() { }
#pragma warning restore CS8618

    private PostAggregate(
        PostId postId,
        string title,
        string content,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(postId)
    {
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static PostAggregate Create(
        string title,
        string content,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new(
            PostId.CreateUnique(),
            title,
            content,
            createdAt,
            updatedAt
        );
    }
}