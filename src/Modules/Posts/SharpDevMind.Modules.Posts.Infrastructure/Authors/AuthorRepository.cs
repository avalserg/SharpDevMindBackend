using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Posts.Domain.Authors;
using SharpDevMind.Modules.Posts.Infrastructure.Database;

namespace SharpDevMind.Modules.Posts.Infrastructure.Authors;

internal sealed class AuthorRepository(PostsDbContext context) : IAuthorRepository
{
    public async Task<Author?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Authors.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Author customer)
    {
        context.Authors.Add(customer);
    }
}
