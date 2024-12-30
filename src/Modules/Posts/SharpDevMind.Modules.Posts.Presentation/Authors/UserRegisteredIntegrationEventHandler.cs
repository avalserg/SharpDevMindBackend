using MediatR;
using SharpDevMind.Common.Application.EventBus;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Authors.CreateAuthor;
using SharpDevMind.Modules.Users.IntegrationEvents;

namespace SharpDevMind.Modules.Posts.Presentation.Authors;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateAuthorCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(CreateAuthorCommand), result.Error);
        }
    }
}
