using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.PostAggregate.ValueObjects;

public sealed class UpvoteId : ValueObject
{
    public Guid Value { get; }

    public UpvoteId(Guid value){
        Value = value;
    }

    public static UpvoteId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}