using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpDevMind.Modules.Quizzes.Domain.QuizResults;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.QuizResults;

internal sealed class QuizResultConfiguration : IEntityTypeConfiguration<QuizResult>
{
    public void Configure(EntityTypeBuilder<QuizResult> builder)
    {
        builder.HasKey(c => c.Id);
    }
}
