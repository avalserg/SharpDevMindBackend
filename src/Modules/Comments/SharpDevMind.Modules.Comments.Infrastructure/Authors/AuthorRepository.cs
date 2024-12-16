using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Comments.Domain.Authors;
using SharpDevMind.Modules.Comments.Infrastructure.Database;

namespace SharpDevMind.Modules.Comments.Infrastructure.Authors;

internal sealed class AuthorRepository(CommentsDbContext context) : IAuthorRepository
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
