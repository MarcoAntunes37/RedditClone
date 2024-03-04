namespace RedditClone.Domain.CommentAggregate;

using RedditClone.Domain.CommentAggregate.Entities;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.CommentAggregate.ValueObjects;
using RedditClone.Domain.Common.ValueObjects;

public sealed class Comment
{
    private readonly List<Votes> _votes = new();
    private readonly List<Replies> _replies = new();
    public CommentId Id { get; private set; }
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
        CommentId id,
        UserId userId,
        PostId postId,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes,
        List<Replies> replies
    )
    {
        Id = id;
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
            new CommentId(Guid.NewGuid()),
            userId,
            postId,
            content,
            createdAt,
            updatedAt,
            votes ?? new(),
            replies ?? new());
    }

    public void UpdateComment(string content)
    {
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    public void AddVote(Votes newVote)
    {
        _votes.Add(newVote);
    }

    public void UpdateVote(VoteId voteId, bool isVoted)
    {
        var vote = _votes.Find(v => v.Id == voteId)!;

        vote.UpdateVote(isVoted);

        _votes.Insert(_votes.FindIndex(v => v.Id == voteId), vote);
    }

    public void RemoveVote(VoteId voteId)
    {
        var vote = _votes.Find(v => v.Id == voteId)!;

        _votes.Remove(vote);
    }

    public void AddReply(Replies newReply)
    {
        _replies.Add(newReply);
    }

    public void UpdateReply(ReplyId replyId, string content)
    {
        var reply = _replies.Find(v => v.Id == replyId)!;

        reply.UpdateReply(content);

        _replies.Insert(_replies.FindIndex(v => v.Id == replyId), reply);
    }

    public void RemoveReply(ReplyId replyId)
    {
        var reply = _replies.Find(v => v.Id == replyId)!;

        _replies.Remove(reply);
    }

    public void AddReplyVote(ReplyId replyId, RepliesVotes votes)
    {
        var reply = _replies.Find(v => v.Id == replyId)!;

        reply.AddReplyVote(votes);
    }

    public void UpdateReplyVote(ReplyId replyId, VoteId voteId, bool isVoted)
    {
        var reply = _replies.Find(v => v.Id == replyId)!;

        reply.UpdateReplyVote(voteId, isVoted);
    }

    public void RemoveReplyVote(ReplyId replyId, VoteId voteId)
    {
        var reply = _replies.Find(v => v.Id == replyId)!;

        reply.RemoveReplyVote(voteId);
    }
}