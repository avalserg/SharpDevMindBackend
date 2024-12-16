using Microsoft.EntityFrameworkCore;
using SharpDevMind.Common.Infrastructure.Outbox;
using SharpDevMind.Modules.Comments.Application.Abstractions.Data;
using SharpDevMind.Modules.Comments.Domain.Authors;
using SharpDevMind.Modules.Comments.Domain.Comments;
using SharpDevMind.Modules.Comments.Domain.Posts;
using SharpDevMind.Modules.Comments.Infrastructure.Authors;
using SharpDevMind.Modules.Comments.Infrastructure.Posts;

namespace SharpDevMind.Modules.Comments.Infrastructure.Database;

public sealed class CommentsDbContext(DbContextOptions<CommentsDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Comment> Comments { get; set; }
    internal DbSet<Author> Authors { get; set; }
    internal DbSet<Post> Posts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Comments);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
    }
}
