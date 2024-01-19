using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate;

public sealed class CommentAggregate :
AggregateRoot<CommentId>
{
    private readonly List<Reply> _replies = new ();
    private readonly List<Reply> _upvotes = new ();
    private readonly List<Reply> _downvotes = new ();
    public string Username { get; }
    public string Content { get; }
    public PostId PostId { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyList<Reply> Replies => _replies.AsReadOnly();
    public IReadOnlyList<Reply> Upvotes => _upvotes.AsReadOnly();
    public IReadOnlyList<Reply> Downvotes => _downvotes.AsReadOnly();

    private CommentAggregate(
        CommentId commentId,
        string username,
        string content,
        PostId postId,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(commentId)
    {
        Username = username;
        Content = content;
        PostId = postId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static CommentAggregate Create(
        string username,
        string content,
        PostId postId,
        DateTime createdAt,
        DateTime updatedAt
    ){
        return new(
            CommentId.CreateUnique(),
            username,
            content,
            postId,
            createdAt,
            updatedAt
        );
    }
}