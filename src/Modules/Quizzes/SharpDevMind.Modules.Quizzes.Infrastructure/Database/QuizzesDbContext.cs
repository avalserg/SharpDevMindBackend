using Microsoft.EntityFrameworkCore;
using SharpDevMind.Common.Infrastructure.Outbox;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.Options;
using SharpDevMind.Modules.Quizzes.Domain.Questions;
using SharpDevMind.Modules.Quizzes.Domain.QuizResults;
using SharpDevMind.Modules.Quizzes.Domain.Users;
using SharpDevMind.Modules.Quizzes.Infrastructure.Options;
using SharpDevMind.Modules.Quizzes.Infrastructure.Questions;
using SharpDevMind.Modules.Quizzes.Infrastructure.QuizResults;
using SharpDevMind.Modules.Quizzes.Infrastructure.Users;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Database;

public sealed class QuizzesDbContext(DbContextOptions<QuizzesDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<QuizResult> QuizResults { get; set; }

    internal DbSet<Question> Questions { get; set; }
    internal DbSet<Option> Options { get; set; }
    internal DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Quizzes);
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OptionConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionConfiguration());
        modelBuilder.ApplyConfiguration(new QuizResultConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());

    }
}
