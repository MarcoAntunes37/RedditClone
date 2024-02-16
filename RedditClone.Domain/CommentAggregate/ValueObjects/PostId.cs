namespace RedditClone.Domain.CommentAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class PostId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

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