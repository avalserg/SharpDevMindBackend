using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Domain.Posts;

public static class PostErrors
{
    public static Error NotFound(Guid postId) =>
        Error.NotFound("Post.NotFound", $"The post with the identifier {postId} was not found");
}
