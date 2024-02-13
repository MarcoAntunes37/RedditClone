namespace RedditClone.Domain.PostAggregate;

using RedditClone.Domain.Common.Models;
using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.PostAggregate.ValueObjects;

public sealed class Post
    : AggregateRoot<PostId>
{
    private readonly List<Votes> _votes = new();
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
        PostId postId,
        CommunityId communityId,
        UserId userId,
        string title,
        string content,
        DateTime createdAt,
        DateTime updatedAt,
        List<Votes> votes
    )
    : base(postId)
    {
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
            PostId.CreateUnique(),
            communityId,
            userId,
            title,
            content,
            createdAt,
            updatedAt,
            votes ?? new());
    }
}