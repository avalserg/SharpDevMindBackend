using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Domain.Posts;

public sealed class PostCreatedDomainEvent(Guid postId) : DomainEvent
{
    public Guid PostId { get; init; } = postId;
}
