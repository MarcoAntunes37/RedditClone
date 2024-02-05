namespace RedditClone.Domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>
where TId : notnull
{
#pragma warning disable CS8604
    public AggregateRoot() : base(default)
    {
    }
#pragma warning restore CS8604
    protected AggregateRoot(TId id) : base(id)
    {
    }
}