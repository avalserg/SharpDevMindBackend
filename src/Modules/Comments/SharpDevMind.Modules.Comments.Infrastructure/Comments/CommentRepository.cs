using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Comments.Domain.Comments;
using SharpDevMind.Modules.Comments.Infrastructure.Database;

namespace SharpDevMind.Modules.Comments.Infrastructure.Comments;

internal sealed class CommentRepository(CommentsDbContext context) : ICommentRepository
{
    public async Task<Comment?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Comments.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Comment post)
    {
        context.Comments.Add(post);
    }
}
