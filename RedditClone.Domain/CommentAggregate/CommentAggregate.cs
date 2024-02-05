using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate;

public sealed class CommentAggregate :
AggregateRoot<CommentId>
{
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private CommentAggregate() { }
#pragma warning restore CS8618

    private CommentAggregate(
        CommentId commentId,
        string content,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(commentId)
    {
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static CommentAggregate Create(
        string content,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new(
            CommentId.CreateUnique(),
            content,
            createdAt,
            updatedAt);
    }

}