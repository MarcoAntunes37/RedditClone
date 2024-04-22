namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.PostAggregate.DomainEvents;

internal sealed class CreatePostDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        PostCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new PostCreatedDomainEvent(
                notification.Id,
                notification.PostId,
                notification.CommunityId,
                notification.UserId));
    }
}
