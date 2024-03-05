namespace RedditClone.Tests;

using RedditClone.Domain.CommunityAggregate;
using RedditClone.Domain.UserAggregate.ValueObjects;

public class DomainCommunityTests
{
    [Fact]
    public void ShouldReturnCommunityObjectOnCreate()
    {
        Guid userId = new("2e053f8e-c75b-48ae-8853-ba0736e61a74");
        string name = "C#";
        string description = "For C# Enjoyers";
        string topic = "Programming";
        DateTime createdAt = DateTime.UtcNow;
        DateTime updatedAt = DateTime.UtcNow;

        var community = Community.Create(
            new UserId(userId),
            name,
            description,
            topic,
            createdAt,
            updatedAt
        );

        Assert.NotNull(community);
        Assert.Equal(name, community.Name);
        Assert.Equal(description, community.Description);
        Assert.Equal(topic, community.Topic);
        Assert.Equal(createdAt, community.CreatedAt);
        Assert.Equal(updatedAt, community.UpdatedAt);
    }

    [Fact]
    public void ShouldReturnCommunityObjectWithNewDataOnUpdate()
    {
        Guid userId = new("2e053f8e-c75b-48ae-8853-ba0736e61a74");
        string name = "C#";
        string description = "For C# Enjoyers";
        string topic = "Programming";
        DateTime createdAt = DateTime.UtcNow;
        DateTime updatedAt = DateTime.UtcNow;

        string newName = "Name updated";
        string newDescription = "Description updated";
        string newTopic = "Topic updated";


        var community = Community.Create(
            new UserId(userId),
            name,
            description,
            topic,
            createdAt,
            updatedAt
        );

        var oldUpdatedAt = community.UpdatedAt;

        community.UpdateCommunity(newName, newDescription, newTopic);


        Assert.NotNull(community);
        Assert.Equal(community.Name, newName);
        Assert.Equal(community.Description, newDescription);
        Assert.Equal(community.Topic, newTopic);
        Assert.True(community.UpdatedAt > oldUpdatedAt);
    }

    [Fact]
    public void ShouldReturnCommunityObjectWithNewPasswordUpdate()
    {
        Guid userId = new("2e053f8e-c75b-48ae-8853-ba0736e61a74");
        string name = "C#";
        string description = "For C# Enjoyers";
        string topic = "Programming";
        DateTime createdAt = DateTime.UtcNow;
        DateTime updatedAt = DateTime.UtcNow;

        Guid newUserId = new("18f82728-92dc-46a1-86cf-3b2b642b8ee1");

        var community = Community.Create(
            new UserId(userId),
            name,
            description,
            topic,
            createdAt,
            updatedAt
        );

        var oldUpdatedAt = community.UpdatedAt;

        community.TransferOwnership(new UserId(newUserId));

        Assert.NotNull(community);
        Assert.Equal(community.UserId, new UserId(newUserId));
        Assert.True(community.UpdatedAt > oldUpdatedAt);
    }
}