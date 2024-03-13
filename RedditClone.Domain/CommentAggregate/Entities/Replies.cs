namespace RedditClone.Domain.CommentAggregate.Entities;

using ErrorOr;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Replies
{
    private readonly List<RepliesVotes> _votes = new();
    public ReplyId Id { get; private set; }
    public UserId UserId { get; private set; }
    public CommunityId CommunityId { get; private set; }
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
        CommunityId communityId,
        CommentId commentId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<RepliesVotes> votes)
    {
        Id = id;
        UserId = userId;
        CommunityId = communityId;
        CommentId = commentId;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _votes = votes;
    }

    public static Replies Create(
        UserId userId,
        CommunityId communityId,
        CommentId commentId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<RepliesVotes> votes)
    {
        return new(
            new ReplyId(Guid.NewGuid()),
            userId,
            communityId,
            commentId,
            content,
            createdAt,
            updatedAt,
            votes);
    }

    public void UpdateReply(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddReplyVote(RepliesVotes votes)
    {
        _votes.Add(votes);
    }

    public void UpdateReplyVote(VoteId voteId, bool isVoted)
    {
        var votes = _votes.Find(v => v.Id == voteId)!;

        votes.UpdateReplyVote(isVoted);

        _votes.Insert(_votes.FindIndex(v => v.Id == voteId), votes);
    }

    public void RemoveReplyVote(VoteId voteId)
    {
        var votes = _votes.Find(v => v.Id == voteId)!;

        _votes.Remove(votes);
    }
}