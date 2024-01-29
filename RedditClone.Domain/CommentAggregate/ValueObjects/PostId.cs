using RedditClone.Domain.Common.Models;

namespace RedditClone.Domain.CommentAggregate.ValueObjects;

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

    public static PostId Create(string PostId){
        Guid guidPostId = new(PostId);

        return new PostId(
            value: guidPostId
        );
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}