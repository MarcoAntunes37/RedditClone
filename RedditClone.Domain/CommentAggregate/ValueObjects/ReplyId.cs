using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.CommentAggregate.ValueObjects;

public sealed class ReplyId : ValueObject
{
    public Guid Value { get; }

    public ReplyId(Guid value){
        Value = value;
    }

    public static ReplyId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}