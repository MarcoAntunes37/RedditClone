namespace RedditClone.Domain.CommunityAggregate;

using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed class Community
    : AggregateRoot<CommunityId, Guid>
{
    public UserId UserId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Topic { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private Community() { }
#pragma warning restore

    private Community(
        CommunityId communityId,
        UserId userId,
        string name,
        string description,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(communityId)
    {
        UserId = userId;
        Name = name;
        Description = description;
        Topic = topic;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Community Create(
        UserId userId,
        string name,
        string description,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new(
            CommunityId.CreateUnique(),
            userId,
            name,
            description,
            topic,
            createdAt,
            updatedAt
        );
    }

    public void UpdateCommunity(
        string name,
        string description,
        string topic)
    {
        Name = name;
        Description = description;
        Topic = topic;
        UpdatedAt = DateTime.UtcNow;
    }
}