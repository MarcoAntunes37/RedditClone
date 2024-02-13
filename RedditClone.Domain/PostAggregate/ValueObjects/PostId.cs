namespace RedditClone.Domain.PostAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class PostId : ValueObject
{
    public Guid Value { get; private set; }

    public PostId(Guid value)
    {
        Value = value;
    }

    public static PostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static PostId Create(Guid guid)
    {
        return new PostId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}