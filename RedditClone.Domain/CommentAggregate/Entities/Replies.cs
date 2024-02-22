namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Replies
{
    private readonly List<RepliesVotes> _votes = new();
    public ReplyId Id { get; private set; }
    public UserId UserId { get; private set; }
    public CommentId CommentId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<RepliesVotes> Votes => _votes.ToList();

#pragma warning disable CS8618
    private Replies() { }
#pragma warning restore CS8618

    private Replies(
        ReplyId id,
        UserId userId,
        CommentId commentId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<RepliesVotes> votes)
    {
        Id = id;
        UserId = userId;
        CommentId = commentId;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _votes = votes;
    }

    public static Replies Create(
        UserId userId,
        CommentId commentId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<RepliesVotes> votes)
    {
        return new(
            new ReplyId(Guid.NewGuid()),
            userId,
            commentId,
            content,
            createdAt,
            updatedAt,
            votes);
    }
}