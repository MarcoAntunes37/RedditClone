namespace RedditClone.Domain.CommentAggregate.Entities;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.DomainEvents;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed class Replies : AggregateRoot
{
    private readonly List<RepliesVotes> _votes = new();
    public new ReplyId Id { get; private set; }
    public UserId UserId { get; private set; }
    public CommunityId CommunityId { get; private set; }
    public CommentId CommentId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public IReadOnlyList<RepliesVotes> Votes => _votes.AsReadOnly();

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
        List<RepliesVotes> votes)
    {
        var reply = new Replies(
            new ReplyId(Guid.NewGuid()),
            userId,
            communityId,
            commentId,
            content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            votes);

        reply.RaiseDomainEvent(
            new ReplyCreatedDomainEvent(
                Guid.NewGuid(),
                reply.Id,
                reply.CommentId,
                reply.CommunityId,
                reply.UserId,
                reply.Content));

        return reply;
    }

    public void UpdateReply(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;

        this.RaiseDomainEvent(
            new ReplyUpdatedDomainEvent(
                Guid.NewGuid(),
                Id,
                CommentId,
                CommunityId,
                UserId,
                Content,
                UpdatedAt));
    }

    public void DeleteReply()
    {
        this.RaiseDomainEvent(
            new ReplyDeletedDomainEvent(Guid.NewGuid(),
                Id,
                CommentId,
                CommunityId,
                UserId));
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

        votes.DeleteReplyVote();

        _votes.Remove(votes);
    }
}