namespace RedditClone.Domain.CommunityAggregate;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.DomainEvents;
using RedditClone.Domain.CommunityAggregate.ValueObjects;

public sealed class Community : AggregateRoot
{
    public new CommunityId Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Topic { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

#pragma warning disable CS8618
    private Community() { }
#pragma warning restore

    private Community(
        CommunityId id,
        string name,
        string description,
        string topic,
        DateTime createdAt,
        DateTime updatedAt
    )
    {
        Id = id;
        Name = name;
        Description = description;
        Topic = topic;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Community Create(
        string name,
        string description,
        string topic,
        UserId userId)
    {

        var community = new Community(
            new CommunityId(Guid.NewGuid()),
            name,
            description,
            topic,
            DateTime.UtcNow,
            DateTime.UtcNow
        );

        community.RaiseDomainEvent(
            new CommunityCreatedDomainEvent(
                Guid.NewGuid(),
                community.Id,
                userId,
                name));

        return community;
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

        this.RaiseDomainEvent(new CommunityUpdatedDomainEvent(Guid.NewGuid(), Id, Name, Description, Topic, UpdatedAt));
    }

    public void DeleteCommunity()
    {
        this.RaiseDomainEvent(new CommunityDeletedDomainEvent(Guid.NewGuid(), Id, Name));
    }
}