using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Posts.Domain.Posts;
using SharpDevMind.Modules.Posts.Infrastructure.Database;

namespace SharpDevMind.Modules.Posts.Infrastructure.Posts;

internal sealed class PostRepository(PostsDbContext context) : IPostRepository
{
    public async Task<Post?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Posts.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Post post)
    {
        context.Posts.Add(post);
    }
}
