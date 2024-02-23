namespace RedditClone.Domain.PostAggregate;

using RedditClone.Domain.CommunityAggregate.ValueObjects;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;
using RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class Post
{
    private readonly List<Votes> _votes = new();
    public PostId Id { get; private set; }
    public CommunityId CommunityId { get; private set; }
    public UserId UserId { get; private set; }
    public string Title { get; private set; }
    public string Content { get; private set; }
    public IReadOnlyList<Votes> Votes => _votes.ToList();
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
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes
    )
    {
        return new(
            new PostId(Guid.NewGuid()),
            communityId,
            userId,
            title,
            content,
            createdAt,
            updatedAt,
            votes ?? new());
    }

    public void UpdatePost(string title, string content)
    {
        Title = title;
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
}