namespace RedditClone.Application.Comment.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommentAggregate.DomainEvents;

internal sealed class CreateVoteDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        VoteCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new VoteCreatedDomainEvent(
                notification.Id,
                notification.VoteId,
                notification.CommentId,
                notification.UserId,
                notification.IsVoted));
    }
}
