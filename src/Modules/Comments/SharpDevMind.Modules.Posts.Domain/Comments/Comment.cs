using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Domain.Comments;

public sealed class Comment : Entity
{
    private Comment()
    {
    }

    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public Guid PostId { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime UpdatedAtUtc { get; private set; }
    public CommentStatus Status { get; private set; }

    public static Comment Create(
        Guid userId,
        Guid postId,
        string content
        )
    {
        var post = new Comment
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            PostId = postId,
            Content = content,
            CreatedAtUtc = DateTime.Now.ToUniversalTime(),
            Status = CommentStatus.Created
        };

        post.Raise(new CommentCreatedDomainEvent(post.Id));

        return post;
    }

    public Result Update(
        Guid userId,
        string title,
        string content
        )
    {
        if (UserId != userId)
        {
            return Result.Failure(CommentErrors.UserNotOwnerComment(userId));
        }

        Content = content;
        UpdatedAtUtc = DateTime.Now.ToUniversalTime();

        Raise(new CommentUpdatedDomainEvent(Id, UpdatedAtUtc));

        return Result.Success();
    }

    public Result Archive()
    {
        if (Status == CommentStatus.Archived)
        {
            return Result.Failure(CommentErrors.AlreadyArchived);
        }

        Status = CommentStatus.Archived;

        Raise(new CommentArchivedDomainEvent(Id));

        return Result.Success();
    }
}
