namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.UserAggregate.DomainEvents;

internal sealed class UserPasswordUpdatedDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        UserPasswordUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new UserPasswordUpdatedDomainEvent(
                notification.Id,
                notification.UserId,
                notification.Password,
                notification.UpdatedAt));
    }
}
