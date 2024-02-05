using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.Domain.CommunityAggregate;

public sealed class CommunityAggregate :
AggregateRoot<CommunityId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Topic { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private CommunityAggregate() { }
#pragma warning restore

    private CommunityAggregate(
        CommunityId communityId,
        string name,
        string description,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(communityId)
    {
        Name = name;
        Description = description;
        Topic = topic;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static CommunityAggregate Create(
        string name,
        string description,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        return new(
            CommunityId.CreateUnique(),
            name,
            description,
            topic,
            createdAt,
            updatedAt
        );
    }
}