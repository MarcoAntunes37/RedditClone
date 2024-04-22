namespace RedditClone.Application.Comment.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommentAggregate.DomainEvents;

internal sealed class DeleteVoteDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        VoteDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new VoteDeletedDomainEvent(
                notification.Id,
                notification.VoteId,
                notification.CommentId,
                notification.UserId));
    }
}
