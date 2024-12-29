using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Quizzes.Domain.Options;
using SharpDevMind.Modules.Quizzes.Domain.Questions;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Options;

internal sealed class OptionConfiguration : IEntityTypeConfiguration<Option>
{
    public void Configure(EntityTypeBuilder<Option> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Text).HasMaxLength(500);
        builder.HasOne<Question>().WithMany(q => q.ListOptions).HasForeignKey(t => t.QuestionId);
    }
}
