namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.PostAggregate.DomainEvents;

internal sealed class UpdatePostDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        PostUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new PostUpdatedDomainEvent(
                notification.Id,
                notification.PostId,
                notification.CommunityId,
                notification.UserId,
                notification.Title,
                notification.Content,
                notification.UpdatedAt));
    }
}
