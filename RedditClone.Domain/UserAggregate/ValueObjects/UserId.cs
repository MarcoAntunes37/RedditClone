namespace RedditClone.Domain.UserAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class UserId : ValueObject
{
    public Guid Value { get; private set; }

    public UserId(Guid value)
    {
        Value = value;
    }

    public static UserId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserId Create(Guid guid)
    {
        return new UserId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}