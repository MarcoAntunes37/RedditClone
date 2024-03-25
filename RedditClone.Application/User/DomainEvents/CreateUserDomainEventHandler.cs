using MediatR;
using Rebus.Bus;
using RedditClone.Domain.UserAggregate;

namespace RedditClone.Application.User.DomainEvents;

internal sealed class CreateUserDomainEventHandler
    : INotificationHandler<UserCreatedDomainEvent>
{
    private IBus _bus;

    public CreateUserDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        await _bus.Send(new UserCreatedDomainEvent(notification.Id, notification.UserId));
    }
}
