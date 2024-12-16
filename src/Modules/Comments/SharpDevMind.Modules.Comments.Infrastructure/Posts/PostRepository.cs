using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Comments.Domain.Posts;
using SharpDevMind.Modules.Comments.Infrastructure.Database;

namespace SharpDevMind.Modules.Comments.Infrastructure.Posts;
internal sealed class PostRepository(CommentsDbContext context) : IPostRepository
{
    public async Task<Post?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Posts.SingleOrDefaultAsync(c => c.PostId == id, cancellationToken);
    }

    public void Insert(Post post)
    {
        context.Posts.Add(post);
    }
}
