namespace RedditClone.Application.Comment.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommentAggregate.DomainEvents;

internal sealed class DeleteVoteOnReplyDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        VoteOnReplyDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _bus.Send(
            new VoteOnReplyDeletedDomainEvent(
                notification.Id,
                notification.VoteId,
                notification.ReplyId,
                notification.UserId));
    }
}
