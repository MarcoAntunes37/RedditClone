using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.PostAggregate.ValueObjects;

public sealed class PostId : ValueObject
{
    public Guid Value { get; }

    public PostId(Guid value){
        Value = value;
    }

    public static PostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}