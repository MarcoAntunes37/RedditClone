namespace RedditClone.Tests.DomainTests;

using RedditClone.Domain.Primitives;
using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;
using RedditClone.Domain.CommunityAggregate.DomainEvents;

public class DomainCommunityTests
{
    [Fact]
    public void Create_Community_ValidInput_Success()
    {
        Guid userId = Guid.NewGuid();
        string name = "C#";
        string description = "For C# Enjoyers";
        string topic = "Programming";

        var community = Community.Create(
            name,
            description,
            topic,
            new UserId(userId));

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)community.GetDomainEvents();

        Assert.NotNull(community);
        Assert.IsType<CommunityCreatedDomainEvent>(domainEvents.LastOrDefault());
        Assert.Equal(name, community.Name);
        Assert.Equal(description, community.Description);
        Assert.Equal(topic, community.Topic);
    }

    [Fact]
    public void Update_Community_ValidInput_Success()
    {
        Guid userId = Guid.NewGuid();
        string name = "C#";
        string description = "For C# Enjoyers";
        string topic = "Programming";

        string newName = "Name updated";
        string newDescription = "Description updated";
        string newTopic = "Topic updated";

        var community = Community.Create(
            name,
            description,
            topic,
            new UserId(userId)
        );

        community.UpdateCommunity(newName, newDescription, newTopic);

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)community.GetDomainEvents();

        Assert.NotNull(community);
        Assert.IsType<CommunityUpdatedDomainEvent>(domainEvents.LastOrDefault());
        Assert.Equal(community.Name, newName);
        Assert.Equal(community.Description, newDescription);
        Assert.Equal(community.Topic, newTopic);
    }

    [Fact]
    public void Delete_Community_ValidInput_Success()
    {
        Guid userId = Guid.NewGuid();
        string name = "C#";
        string description = "For C# Enjoyers";
        string topic = "Programming";

        var community = Community.Create(
            name,
            description,
            topic,
            new UserId(userId));

        community.DeleteCommunity();

        List<IDomainEvent> domainEvents = (List<IDomainEvent>)community.GetDomainEvents();

        Assert.NotNull(community);
        Assert.IsType<CommunityDeletedDomainEvent>(domainEvents.LastOrDefault());
    }
}