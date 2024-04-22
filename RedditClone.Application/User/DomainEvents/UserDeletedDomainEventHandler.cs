namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.UserAggregate.DomainEvents;

internal sealed class UserDeletedDomainEventHandler(IBus bus)
{

    private readonly IBus _bus = bus;

    public async Task Handle(
        UserDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new UserDeletedDomainEvent(notification.Id, notification.UserId));
    }
}
