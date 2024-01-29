using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.CommentAggregate.ValueObjects;

public sealed class UserId : ValueObject
{
    public Guid Value { get; }

    public UserId(Guid value){
        Value = value;
    }

    public static UserId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserId Create(string userId){
        Guid guidUserId = new(userId);

        return new UserId(
            value: guidUserId
        );
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}