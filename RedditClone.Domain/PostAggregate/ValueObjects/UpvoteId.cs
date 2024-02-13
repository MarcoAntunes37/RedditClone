namespace RedditClone.Domain.PostAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class UpvoteId : ValueObject
{
    public Guid Value { get; private set; }

    public UpvoteId(Guid value)
    {
        Value = value;
    }

    public static UpvoteId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UpvoteId Create(Guid guid)
    {
        return new UpvoteId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}