namespace RedditClone.Application.Comment.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommentAggregate.DomainEvents;

internal sealed class CreateReplyDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        ReplyCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null
        || notification.CommunityId == null)
        {
            return;
        }

        await _bus.Send(
            new ReplyCreatedDomainEvent(
                notification.Id,
                notification.ReplyId,
                notification.CommentId,
                notification.CommunityId,
                notification.UserId,
                notification.Content));
    }
}
