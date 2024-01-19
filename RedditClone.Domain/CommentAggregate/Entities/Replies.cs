using RedditClone.Domain.Common.Models;
using RedditClone.Domain.Comment.ValueObjects;

namespace RedditClone.Domain.Comment.Entities;

public sealed class Reply : 
Entity<ReplyId>
{
    private readonly List<Upvotes> _upvotes = new();
    private readonly List<Downvotes> _downvotes = new();
    public string Name { get; }
    public string Description { get; }
    public string Topic { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    public IReadOnlyList<Upvotes> Upvotes => _upvotes.AsReadOnly(); 
    public IReadOnlyList<Downvotes> Downvotes => _downvotes.AsReadOnly();
    private Reply(
        ReplyId replyId, 
        string name, 
        string description, 
        string topic,
        DateTime createdAt,
        DateTime updatedAt) : base(replyId)
    {
        Name = name;
        Description = description;
        Topic = topic;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Reply Create(
        string name, 
        string description, 
        string topic,
        DateTime createdAt,
        DateTime updatedAt)
    {
        return new(
            ReplyId.CreateUnique(),
            name,
            description,
            topic,
            createdAt,
            updatedAt);
    }
}