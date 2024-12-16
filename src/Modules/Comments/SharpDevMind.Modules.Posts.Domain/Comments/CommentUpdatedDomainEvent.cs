using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Domain.Comments;

public sealed class CommentUpdatedDomainEvent(Guid commentId, DateTime updateAtUtc)
    : DomainEvent
{
    public Guid CommentId { get; } = commentId;

    public DateTime UpdateAtUtc { get; } = updateAtUtc;

}
