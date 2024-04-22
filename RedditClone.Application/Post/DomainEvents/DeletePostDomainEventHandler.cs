namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.PostAggregate.DomainEvents;

internal sealed class DeletePostDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        PostDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new PostDeletedDomainEvent(
                notification.Id,
                notification.PostId,
                notification.CommunityId,
                notification.UserId));
    }
}
