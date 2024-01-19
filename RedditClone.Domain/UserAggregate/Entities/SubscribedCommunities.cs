using RedditClone.Domain.Common.Models;
using RedditClone.Domain.UserAggregate.ValueObjects;

namespace RedditClone.Domain.UserAggregate.Entities;

public sealed class SubscribedCommunity :
Entity<SubscribedCommunityId>
{
    public string Name { get; }
    public string Description { get; }
    public string Topic { get; }

    private SubscribedCommunity(
        SubscribedCommunityId subscribedCommunityId,
        string name,
        string description,
        string topic) : base(subscribedCommunityId)
    {
        Name = name;
        Description = description;
        Topic = topic;
    }

    public static SubscribedCommunity Create(
        string name,
        string description,
        string topic)
    {
        return new(
            SubscribedCommunityId.CreateUnique(),
            name,
            description,
            topic);
    }
}