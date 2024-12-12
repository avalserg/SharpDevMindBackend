using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Posts.Domain.Authors;

namespace SharpDevMind.Modules.Posts.Infrastructure.Authors;

internal sealed class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.FirstName).HasMaxLength(200);

        builder.Property(c => c.LastName).HasMaxLength(200);

        builder.Property(c => c.Email).HasMaxLength(300);
    }
}
