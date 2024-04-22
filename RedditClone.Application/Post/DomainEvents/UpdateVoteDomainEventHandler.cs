namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.PostAggregate.DomainEvents;

internal sealed class UpdateVoteDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        VoteUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new VoteUpdatedDomainEvent(
                notification.Id,
                notification.VoteId,
                notification.PostId,
                notification.UserId,
                notification.IsVoted));
    }
}
