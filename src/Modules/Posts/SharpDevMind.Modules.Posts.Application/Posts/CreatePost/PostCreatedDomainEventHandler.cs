using MediatR;
using SharpDevMind.Common.Application.EventBus;
using SharpDevMind.Common.Application.Exceptions;
using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.Application.Posts.GetPost;
using SharpDevMind.Modules.Posts.Domain.Posts;
using SharpDevMind.Modules.Posts.IntegrationEvents;

namespace SharpDevMind.Modules.Posts.Application.Posts.CreatePost;

internal sealed class PostCreatedDomainEventHandler(ISender sender, IEventBus eventBus)
    : DomainEventHandler<PostCreatedDomainEvent>
{
    public override async Task Handle(
        PostCreatedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result<PostResponse> result = await sender.Send(new GetPostQuery(notification.PostId), cancellationToken);

        if (result.IsFailure)
        {
            throw new SharpDevMindException(nameof(GetPostQuery), result.Error);
        }

        await eventBus.PublishAsync(
            new PostCreatedIntegrationEvent(
                notification.Id,
                notification.OccurredOnUtc,
                result.Value.Id,
                result.Value.AuthorId
                ),
            cancellationToken);

    }

}
