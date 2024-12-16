using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Domain.Comments;

public static class CommentErrors
{
    public static Error NotFound(Guid commentId) =>
        Error.NotFound("Comment.NotFound", $"The comment with the identifier {commentId} was not found");
    public static Error OwnerNotFound(Guid userId) =>
        Error.NotFound("Owner.NotFound", $"The user with the identifier {userId} was not found");

    public static readonly Error AlreadyArchived = Error.Problem(
        "Comment.AlreadyArchived",
        "The comment was already archived");

    public static Error UserNotOwnerComment(Guid userId) => Error.Problem(
        "Comment.UserNotOwnerComment",
        $"The comment weren`t created by user with id {userId}");

}
