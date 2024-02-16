namespace RedditClone.Domain.PostAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class CommunityId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public CommunityId(Guid value)
    {
        Value = value;
    }

    public static CommunityId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CommunityId Create(Guid guid)
    {
        return new CommunityId(guid);
    }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}