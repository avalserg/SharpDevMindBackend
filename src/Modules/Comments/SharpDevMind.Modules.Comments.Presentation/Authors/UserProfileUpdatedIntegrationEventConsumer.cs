using MassTransit;
using MediatR;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Authors.UpdateAuthor;
using SharpDevMind.Modules.Users.IntegrationEvents;

namespace SharpDevMind.Modules.Comments.Presentation.Authors;

public sealed class UserProfileUpdatedIntegrationEventConsumer(ISender sender)
    : IConsumer<UserProfileUpdatedIntegrationEvent>
{
    public async Task Consume(ConsumeContext<UserProfileUpdatedIntegrationEvent> context)
    {
        Result result = await sender.Send(
            new UpdateAuthorCommand(
                context.Message.UserId,
                context.Message.FirstName,
                context.Message.LastName),
            context.CancellationToken);

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(UpdateAuthorCommand), result.Error);
        }
    }
}
