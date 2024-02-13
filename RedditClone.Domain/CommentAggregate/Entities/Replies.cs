namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommentAggregate.ValueObjects;

public sealed class Replies
    :Entity<ReplyId>
{
    private readonly List<Votes> _votes = new();
    public UserId UserId { get; private set; }
    public string Username { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<Votes> Votes => _votes.ToList();

#pragma warning disable CS8618
    private Replies() { }
#pragma warning restore CS8618

    private Replies(
        ReplyId replyId,
        UserId userId,
        string username,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes) : base(replyId)
    {
        UserId = userId;
        Username = username;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _votes = votes;
    }

    public static Replies Create(
        UserId userId,
        string username,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes)
    {
        return new(
            ReplyId.CreateUnique(),
            userId,
            username,
            content,
            createdAt,
            updatedAt,
            votes);
    }
}