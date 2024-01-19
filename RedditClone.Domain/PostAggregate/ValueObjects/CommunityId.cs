using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.Post.ValueObjects;

public sealed class CommunityId : ValueObject
{
    public Guid Value { get; }

    public CommunityId(Guid value){
        Value = value;
    }

    public static CommunityId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}