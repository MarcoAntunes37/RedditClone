namespace RedditClone.Application.User.DomainEvents;

using Rebus.Bus;
using RedditClone.Domain.UserAggregate.DomainEvents;
using RedditClone.Application.Common.Interfaces.Services;

internal sealed class CreateUserDomainEventHandler(IBus bus, IEmailService emailService)
{
    private readonly IBus _bus = bus;
    private readonly IEmailService _emailService = emailService;

    public async Task Handle(
        UserCreatedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        if (notification.UserId == null)
        {
            return;
        }

        await _emailService.SendWelcomeEmailAsync(
            notification.Email, notification.Username, cancellationToken);

        await _bus.Send(
            new UserCreatedDomainEvent(
                notification.Id,
                notification.UserId,
                notification.Email,
                notification.Username));
    }
}
