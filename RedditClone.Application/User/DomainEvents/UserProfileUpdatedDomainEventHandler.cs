namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.UserAggregate.DomainEvents;

internal sealed class UserProfileUpdatedDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        UserProfileUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new UserProfileUpdatedDomainEvent(
                notification.Id,
                notification.UserId,
                notification.Firstname,
                notification.Lastname,
                notification.Email,
                notification.UpdatedAt));
    }
}
