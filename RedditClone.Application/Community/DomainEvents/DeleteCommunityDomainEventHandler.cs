namespace RedditClone.Application.Community.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommunityAggregate.DomainEvents;

internal sealed class DeleteCommunityDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        CommunityDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.CommunityId == null)
        {
            return;
        }

        await _bus.Send(
            new CommunityDeletedDomainEvent(
                notification.Id,
                notification.CommunityId,
                notification.Name));
    }
}
