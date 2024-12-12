using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Posts.Domain.Categories;
using SharpDevMind.Modules.Posts.Infrastructure.Database;

namespace SharpDevMind.Modules.Posts.Infrastructure.Categories;

internal sealed class CategoryRepository(PostsDbContext context) : ICategoryRepository
{
    public async Task<Category?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Categories.SingleOrDefaultAsync(e => e.Id == id, cancellationToken);
    }

    public void Insert(Category category)
    {
        context.Categories.Add(category);
    }
}
