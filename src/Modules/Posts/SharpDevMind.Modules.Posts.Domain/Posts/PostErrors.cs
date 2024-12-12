using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Posts.Domain.Posts;

public static class PostErrors
{
    public static Error NotFound(Guid postId) =>
        Error.NotFound("Post.NotFound", $"The post with the identifier {postId} was not found");
    public static Error OwnerNotFound(Guid userId) =>
        Error.NotFound("Owner.NotFound", $"The user with the identifier {userId} was not found");

    public static readonly Error AlreadyArchived = Error.Problem(
        "Post.AlreadyArchived",
        "The post was already archived");

    public static Error UserNotOwnerPost(Guid userId) => Error.Problem(
        "Post.UserNotOwnerPost",
        $"The post weren`t created by user with id {userId}");

}
