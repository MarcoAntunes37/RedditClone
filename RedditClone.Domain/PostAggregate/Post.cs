namespace RedditClone.Domain.PostAggregate;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.Common.ValueObjects;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.DomainEvents;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed class Post : AggregateRoot
{
    private readonly List<Votes> _votes = new();
    public new PostId Id { get; private set; }
    public CommunityId CommunityId { get; private set; }
    public UserId UserId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public IReadOnlyList<Votes> Votes => _votes.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private Post() { }
#pragma warning restore CS8618

    private Post(
        PostId id,
        CommunityId communityId,
        UserId userId,
        string title,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes
    )
    {
        Id = id;
        CommunityId = communityId;
        UserId = userId;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _votes = votes;
    }

    public static Post Create(
        CommunityId communityId,
        UserId userId,
        string title,
        string content,
        List<Votes> votes
    )
    {
        var post = new Post(
            new PostId(Guid.NewGuid()),
            communityId,
            userId,
            title,
            content,
            DateTime.UtcNow,
            DateTime.UtcNow,
            votes ?? new());

        post.RaiseDomainEvent(
            new PostCreatedDomainEvent(
                Guid.NewGuid(),
                post.Id,
                post.CommunityId,
                post.UserId));

        return post;
    }

    public void UpdatePost(string title, string content)
    {
        Title = title;
        Content = content;
        UpdatedAt = DateTime.UtcNow;

        this.RaiseDomainEvent(
            new PostUpdatedDomainEvent(
                Guid.NewGuid(),
                Id,
                CommunityId,
                UserId,
                Title,
                Content,
                UpdatedAt));
    }

    public void DeletePost()
    {
        RaiseDomainEvent(
            new PostDeletedDomainEvent(
                Guid.NewGuid(),
                Id,
                CommunityId,
                UserId));
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

        vote.DeleteVote();

        _votes.Remove(vote);
    }
}