using RedditClone.Domain.PostAggregate.Entities;
using RedditClone.Domain.Common.Models;
using RedditClone.Domain.PostAggregate.ValueObjects;

namespace RedditClone.Domain.PostAggregate;

public sealed class PostAggregate :
AggregateRoot<PostId>
{
    private readonly List<Upvotes> _upvotes = new();
    private readonly List<Downvotes> _downvotes = new();
    public string Title { get; }
    public string Content { get; }
    public UserId UserId { get; }
    public CommunityId CommunityId { get; }
    public IReadOnlyList<Upvotes> Upvotes => _upvotes.AsReadOnly();
    public IReadOnlyList<Downvotes> Downvotes => _downvotes.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    private PostAggregate(
        PostId postId,
        string title,
        string content,
        UserId userId,
        CommunityId communityId,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(postId)
    {
        Title = title;
        Content = content;
        UserId = userId;
        CommunityId = communityId;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static PostAggregate Create(
        string title,
        string content,
        UserId userId,
        CommunityId communityId,
        DateTime createdAt,
        DateTime updatedAt
    ){
        return new(
            PostId.CreateUnique(),
            title,
            content,
            userId,
            communityId,
            createdAt,
            updatedAt
        );
    }
}