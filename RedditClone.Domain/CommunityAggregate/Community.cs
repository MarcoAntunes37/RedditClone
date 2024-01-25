using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.Domain.CommunityAggregate;

public sealed class CommunityAggregate :
AggregateRoot<CommunityId>
{
    public UserId UserId { get; }
    public string Name { get; }
    public string Description { get; }
    public int MembersCount { get; }
    public string Topic { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    private CommunityAggregate(
        CommunityId communityId,
        UserId userId,
        string name,
        string description,
        int membersCount,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(communityId)
    {
        UserId = userId;
        Name = name;
        Description = description;
        MembersCount = membersCount;
        Topic = topic;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static CommunityAggregate Create(
        UserId userId,
        string name,
        string description,
        int membersCount,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    ){
        return new(
            CommunityId.CreateUnique(),
            userId,
            name,
            description,
            membersCount,
            topic,
            createdAt,
            updatedAt
        );
    }
}