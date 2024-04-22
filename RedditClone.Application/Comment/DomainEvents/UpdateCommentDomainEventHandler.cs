namespace RedditClone.Application.Comment.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.CommentAggregate.DomainEvents;

internal sealed class UpdateCommentDomainEventHandler(IBus bus)
{
    private readonly IBus _bus = bus;

    public async Task Handle(
        CommentUpdatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null
        || notification.CommunityId == null)
        {
            return;
        }

        await _bus.Send(
            new CommentUpdatedDomainEvent(
                notification.Id,
                notification.CommentId,
                notification.PostId,
                notification.CommunityId,
                notification.UserId,
                notification.Content,
                notification.UpdatedAt));
    }
}
