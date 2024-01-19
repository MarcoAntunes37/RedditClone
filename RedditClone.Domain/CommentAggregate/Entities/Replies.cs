using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;

namespace RedditClone.Domain.CommentAggregate.Entities;

public sealed class Reply :
Entity<ReplyId>
{
    private readonly List<Upvotes> _upvotes = new();
    private readonly List<Downvotes> _downvotes = new();
    public UserId UserId { get; }
    public string Username { get; }
    public string Content { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyList<Upvotes> Upvotes =>
        _upvotes.AsReadOnly();
    public IReadOnlyList<Downvotes> Downvotes =>
        _downvotes.AsReadOnly();
    private Reply(
        ReplyId replyId,
        UserId userId,
        string username,
        string content,
        DateTime createdAt,
        DateTime updatedAt) : base(replyId)
    {
        UserId = userId;
        Username = username;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Reply Create(
        UserId userId,
        string username,
        string content,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new(
            ReplyId.CreateUnique(),
            userId,
            username,
            content,
            createdAt,
            updatedAt);
    }
}