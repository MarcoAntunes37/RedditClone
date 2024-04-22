namespace RedditClone.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = new();

    protected AggregateRoot(Guid Id)
        : base(Id)
    { }

    protected AggregateRoot()
    { }

    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents;

    public void ClearDomainEvents() => _domainEvents.Clear();

    protected void RaiseDomainEvent(IDomainEvent domainEvent) =>
        _domainEvents.Add(domainEvent);
}