using MassTransit;
using MediatR;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Users.CreateUser;
using SharpDevMind.Modules.Users.IntegrationEvents;

namespace SharpDevMind.Modules.Quizzes.Presentation.Users;

public sealed class UserRegisteredIntegrationEventConsumer(ISender sender)
    : IConsumer<UserRegisteredIntegrationEvent>
{

    public async Task Consume(ConsumeContext<UserRegisteredIntegrationEvent> context)
    {
        Result result = await sender.Send(new CreateUserCommand(
            context.Message.UserId,
            context.Message.Email,
            context.Message.FirstName,
            context.Message.LastName));

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(CreateUserCommand), result.Error);
        }
    }
}
