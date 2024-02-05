using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.PostAggregate.ValueObjects;

public sealed class CommunityId : ValueObject
{
    public Guid Value { get; }

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