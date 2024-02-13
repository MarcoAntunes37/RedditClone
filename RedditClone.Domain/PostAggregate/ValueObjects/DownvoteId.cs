namespace RedditClone.Domain.PostAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class DownvoteId : ValueObject
{
    public Guid Value { get; private set; }

    public DownvoteId(Guid value)
    {
        Value = value;
    }

    public static DownvoteId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static DownvoteId Create(Guid guid)
    {
        return new DownvoteId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}