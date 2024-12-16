using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Comments.Domain.Comments;
using SharpDevMind.Modules.Comments.Domain.Posts;

namespace SharpDevMind.Modules.Comments.Infrastructure.Posts;


internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasKey(c => c.PostId);
        builder.HasMany<Comment>().WithOne();

    }
}
