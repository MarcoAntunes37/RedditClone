using RedditClone.Domain.Common.Models;
using RedditClone.Domain.Post.ValueObjects;
using RedditClone.Domain.User.Entities;

namespace RedditClone.Domain.Post;

public sealed class Post : 
AggregateRoot<PostId>
{
    private readonly List<Upvotes> _upvotes = new();
    private readonly List<Upvotes> _downvotes = new();
    public string Title { get; }
    public string Content { get; }
    public UserId UserId { get; }
    public CommunityId CommunityId { get; }
    public IReadOnlyList<Upvotes> Upvotes => _upvotes.AsReadOnly();
    public IReadOnlyList<Upvotes> Downvotes => _downvotes.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    private Post(
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

    public static Post Create(        
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