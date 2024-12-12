using MediatR;
using SharpDevMind.Common.Application.EventBus;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Users.Application.Users.GetUser;
using SharpDevMind.Modules.Users.Domain.Users;
using SharpDevMind.Modules.Users.IntegrationEvents;

namespace SharpDevMind.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus eventBus)
    : IDomainEventHandler<UserRegisteredDomainEvent>
{
    public async Task Handle(
        UserRegisteredDomainEvent notification,
        CancellationToken cancellationToken)
    {
        Result<UserResponse> result = await sender.Send(new GetUserQuery(notification.UserId), cancellationToken);

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(GetUserQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);

    }

}
