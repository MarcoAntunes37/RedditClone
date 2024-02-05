using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.CommentAggregate.ValueObjects;

public sealed class CommentId : ValueObject
{
    public Guid Value { get; }

    public CommentId(Guid value)
    {
        Value = value;
    }

    public static CommentId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CommentId Create(Guid guid)
    {
        return new CommentId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}