using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.Common.Models;
using RedditClone.Domain.PostAggregate.ValueObjects;

namespace RedditClone.Domain.PostAggregate;

public sealed class PostAggregate :
AggregateRoot<PostId>
{
    private readonly List<Upvotes> _upvotes = new();
    private readonly List<Downvotes> _downvotes = new();
    public string Title { get; private set; }
    public string Content { get; private set; }
    public UserId UserId { get; private set; }
    public CommunityId CommunityId { get; private set; }
    public IReadOnlyList<Upvotes> Upvotes => _upvotes.AsReadOnly();
    public IReadOnlyList<Downvotes> Downvotes => _downvotes.AsReadOnly();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private PostAggregate() { }
#pragma warning restore CS8618

    private PostAggregate(
        PostId postId,
        string title,
        string content,
        UserId userId,
        CommunityId communityId,
        DateTime createdAt,
        DateTime updatedAt,
        List<Upvotes> upvotes,
        List<Downvotes> downvotes
    )
    : base(postId)
    {
        Title = title;
        Content = content;
        UserId = userId;
        CommunityId = communityId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
        _upvotes = upvotes ?? new List<Upvotes>();
        _downvotes = downvotes ?? new List<Downvotes>();
    }

    public static PostAggregate Create(
        string title,
        string content,
        UserId userId,
        CommunityId communityId,
        DateTime createdAt,
        DateTime updatedAt,
        List<Upvotes> upvotes,
        List<Downvotes> downvotes
    )
    {
        return new(
            PostId.CreateUnique(),
            title,
            content,
            userId,
            communityId,
            createdAt,
            updatedAt,
            upvotes,
            downvotes
        );
    }
}