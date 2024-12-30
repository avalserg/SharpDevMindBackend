using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Comments.Domain.Authors;
using SharpDevMind.Modules.Comments.Domain.Comments;

namespace SharpDevMind.Modules.Comments.Infrastructure.Authors;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(c => c.Id);

        builder.HasMany<Comment>().WithOne();

        builder.Property(c => c.FirstName).HasMaxLength(200);

        builder.Property(c => c.LastName).HasMaxLength(200);

    }
}
