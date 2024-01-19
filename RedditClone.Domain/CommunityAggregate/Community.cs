using RedditClone.Domain.Common.Models;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

namespace RedditClone.Domain.CommunityAggregate;

public sealed class Community :
AggregateRoot<CommunityId>
{
    public string Name { get; }
    public string Description { get; }
    public int MembersCount { get; }
    public string Topic { get; }
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }
    private Community(
        CommunityId communityId,
        string name,
        string description,
        int membersCount,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    : base(communityId)
    {
        Name = name;
        Description = description;
        MembersCount = membersCount;
        Topic = topic;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Community Create(
        string name,
        string description,
        int membersCount,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    ){
        return new(
            CommunityId.CreateUnique(),
            name,
            description,
            membersCount,
            topic,
            createdAt,
            updatedAt
        );
    }
}