using MassTransit;
using MediatR;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Comments.Application.Posts.CreatePost;
using SharpDevMind.Modules.Posts.IntegrationEvents;

namespace SharpDevMind.Modules.Comments.Presentation.Posts;

public sealed class PostCreatedIntegrationEventConsumer(ISender sender)
    : IConsumer<PostCreatedIntegrationEvent>
{

    public async Task Consume(ConsumeContext<PostCreatedIntegrationEvent> context)
    {
        Result result = await sender.Send(new CreatePostCommand(
            context.Message.PostId,
            context.Message.AuthorId
            ));

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(CreatePostCommand), result.Error);
        }
    }
}
