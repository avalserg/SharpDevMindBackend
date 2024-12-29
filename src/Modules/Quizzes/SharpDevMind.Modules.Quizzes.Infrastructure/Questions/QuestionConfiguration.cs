using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Quizzes.Domain.Questions;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Questions;

internal sealed class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.TextCondition).HasMaxLength(2000);
        builder.HasMany(q => q.ListOptions)
            .WithOne()
            .HasForeignKey(o => o.QuestionId);
    }
}
