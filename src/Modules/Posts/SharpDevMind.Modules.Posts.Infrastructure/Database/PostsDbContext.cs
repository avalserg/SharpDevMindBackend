using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Posts.Application.Abstractions.Data;
using SharpDevMind.Modules.Posts.Domain.Categories;
using SharpDevMind.Modules.Posts.Domain.Posts;
using SharpDevMind.Modules.Posts.Infrastructure.Posts;

namespace SharpDevMind.Modules.Posts.Infrastructure.Database;

public sealed class PostsDbContext(DbContextOptions<PostsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Post> Posts { get; set; }

    internal DbSet<Category> Categories { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Posts);

        modelBuilder.ApplyConfiguration(new PostConfiguration());
    }
}
