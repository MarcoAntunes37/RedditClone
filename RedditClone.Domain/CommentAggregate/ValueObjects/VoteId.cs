namespace RedditClone.Domain.CommentAggregate.ValueObjects;

using RedditClone.Domain.Common.Models;

public sealed class VoteId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public VoteId(Guid value)
    {
        Value = value;
    }

    public static VoteId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static VoteId Create(Guid guid)
    {
        return new VoteId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}