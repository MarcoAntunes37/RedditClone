using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.UserAggregate.ValueObjects;

public sealed class SubscribedCommunityId : ValueObject
{
    public Guid Value { get; }

    public SubscribedCommunityId(Guid value){
        Value = value;
    }

    public static SubscribedCommunityId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}