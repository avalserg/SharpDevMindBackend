using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Domain.Comments;
public sealed class CommentArchivedDomainEvent(Guid commentId) : DomainEvent
{
    public Guid CommentId { get; init; } = commentId;
}
