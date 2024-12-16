using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Comments.Domain.Posts;
public sealed class Post : Entity
{
    private Post()
    {
    }

    public Guid PostId { get; private set; }
    public Guid AuthorId { get; private set; }



    public static Post Create(Guid postId, Guid authorId)
    {
        return new Post
        {
            PostId = postId,
            AuthorId = authorId,

        };
    }

}
