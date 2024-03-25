namespace RedditClone.Domain.Primitives;

public abstract class Entity
{
    private readonly List<DomainEvent> _domainEvent = new();
    public ICollection<DomainEvent> DomainEvents => _domainEvent;

    protected void Raise(DomainEvent domainEvent)
    {
        _domainEvent.Add(domainEvent);
    }

}