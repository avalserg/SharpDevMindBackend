using MediatR;
using SharpDevMind.Common.Application.EventBus;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Authors.UpdateAuthor;
using SharpDevMind.Modules.Users.IntegrationEvents;

namespace SharpDevMind.Modules.Posts.Presentation.Authors;

internal sealed class UserProfileUpdatedIntegrationEventHandler(ISender sender)
    : IntegrationEventHandler<UserProfileUpdatedIntegrationEvent>
{
    public override async Task Handle(
        UserProfileUpdatedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new UpdateAuthorCommand(
                integrationEvent.UserId,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(UpdateAuthorCommand), result.Error);
        }
    }
}
