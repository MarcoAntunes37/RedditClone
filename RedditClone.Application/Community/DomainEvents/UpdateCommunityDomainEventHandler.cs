namespace RedditClone.Application.Community.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommunityAggregate.DomainEvents;

internal sealed class UpdateCommunityDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        CommunityUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.CommunityId == null)
        {
            return;
        }

        await _bus.Send(
            new CommunityUpdatedDomainEvent(
                notification.Id,
                notification.CommunityId,
                notification.Name,
                notification.Description,
                notification.Topic,
                notification.UpdatedAt));
    }
}
