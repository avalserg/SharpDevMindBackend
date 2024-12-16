using SharpDevMind.Common.Application.EventBus;

namespace SharpDevMind.Modules.Posts.IntegrationEvents;

public sealed class PostCreatedIntegrationEvent : IntegrationEvent
{
    public PostCreatedIntegrationEvent(
        Guid id,
        DateTime occurredOnUtc,
        Guid postId,
        Guid authorId
       )
        : base(id, occurredOnUtc)
    {
        PostId = postId;
        AuthorId = authorId;

    }

    public Guid PostId { get; init; }
    public Guid AuthorId { get; init; }

}
