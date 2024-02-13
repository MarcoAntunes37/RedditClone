using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.Entities;

namespace RedditClone.Domain.CommentAggregate;

public sealed class Comment
    : AggregateRoot<CommentId>
{
    private readonly List<Votes> _votes = new();
    private readonly List<Replies> _replies = new();
    public UserId UserId { get; private set; }
    public PostId PostId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Votes> Votes => _votes.ToList();
    public IReadOnlyList<Replies> Replies => _replies.ToList();

#pragma warning disable CS8618
    private Comment() { }
#pragma warning restore CS8618

    private Comment(
        CommentId commentId,
        UserId userId,
        PostId postId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes,
        List<Replies> replies
    )
    : base(commentId)
    {
        UserId = userId;
        PostId = postId;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _votes = votes;
        _replies = replies;
    }

    public static Comment Create(
        UserId userId,
        PostId postId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes,
        List<Replies> replies
    )
    {
        return new(
            CommentId.CreateUnique(),
            userId,
            postId,
            content,
            createdAt,
            updatedAt,
            votes ?? new(),
            replies ?? new());
    }

}