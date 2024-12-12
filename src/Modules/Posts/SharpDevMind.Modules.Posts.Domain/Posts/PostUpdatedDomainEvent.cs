using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Domain.Posts;

public sealed class PostUpdatedDomainEvent(Guid postId, DateTime updateAtUtc)
    : DomainEvent
{
    public Guid PostId { get; } = postId;

    public DateTime UpdateAtUtc { get; } = updateAtUtc;

}
