using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Posts.Domain.Categories;
using SharpDevMind.Modules.Posts.Domain.Posts;

namespace SharpDevMind.Modules.Posts.Infrastructure.Posts;

internal sealed class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.HasOne<Category>().WithMany();
    }
}
