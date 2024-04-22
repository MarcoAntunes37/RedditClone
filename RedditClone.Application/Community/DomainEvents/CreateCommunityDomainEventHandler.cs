namespace RedditClone.Application.Community.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommunityAggregate.DomainEvents;

internal sealed class CreateCommunityDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        CommunityCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null
        || notification.CommunityId == null)
        {
            return;
        }

        await _bus.Send(new CommunityCreatedDomainEvent(
            notification.Id, notification.CommunityId, notification.UserId, notification.Name));
    }
}
