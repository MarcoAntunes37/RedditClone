using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.CommentAggregate.ValueObjects;

public sealed class ReplyId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set;}

    public ReplyId(Guid value){
        Value = value;
    }

    public static ReplyId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static ReplyId Create(Guid guid)
    {
        return new ReplyId(guid);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}