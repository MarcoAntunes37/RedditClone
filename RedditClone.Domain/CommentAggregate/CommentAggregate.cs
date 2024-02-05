using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate;

public sealed class CommentAggregate :
AggregateRoot<CommentId>
{
    private readonly List<Replies> _replies = new();
    private readonly List<Upvotes> _upvotes = new();
    private readonly List<Downvotes> _downvotes = new();
    public string Content { get; private set; }
    public UserId UserId { get; private set; }
    public PostId PostId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Replies> Replies => _replies.AsReadOnly();
    public IReadOnlyList<Upvotes> Upvotes => _upvotes.AsReadOnly();
    public IReadOnlyList<Downvotes> Downvotes => _downvotes.AsReadOnly();

#pragma warning disable CS8618
    private CommentAggregate() { }
#pragma warning restore CS8618

    private CommentAggregate(
        CommentId commentId,
        UserId userId,
        PostId postId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Replies> replies,
        List<Upvotes> upvotes,
        List<Downvotes> downvotes
    )
    : base(commentId)
    {
        UserId = userId;
        PostId = postId;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _replies = replies ?? new List<Replies>();
        _upvotes = upvotes ?? new List<Upvotes>();
        _downvotes = downvotes ?? new List<Downvotes>();
    }

    public static CommentAggregate Create(
        string content,
        UserId userId,
        PostId postId,
        DateTime createdAt,
        DateTime updatedAt,
        List<Replies> replies,
        List<Upvotes> upvotes,
        List<Downvotes> downvotes
    )
    {
        return new(
            CommentId.CreateUnique(),
            userId,
            postId,
            content,
            createdAt,
            updatedAt,
            replies,
            upvotes,
            downvotes
        );
    }

}