using MassTransit;
using MediatR;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Authors.CreateAuthor;
using SharpDevMind.Modules.Users.IntegrationEvents;

namespace SharpDevMind.Modules.Comments.Presentation.Authors;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{

    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(new CreateAuthorCommand(
            context.Message.UserId,
            context.Message.Email,
            context.Message.FirstName,
            context.Message.LastName));

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(CreateAuthorCommand), result.Error);
        }
    }
}
