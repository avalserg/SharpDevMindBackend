using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Quizzes.Domain.QuizResults;
using SharpDevMind.Modules.Quizzes.Infrastructure.Database;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.QuizResults;
internal sealed class QuizResultRepository(QuizzesDbContext context) : IQuizResultRepository
{
    public async Task<QuizResult?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.QuizResults.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    }

    public void Insert(QuizResult quizResult)
    {
        context.QuizResults.Add(quizResult);

    }
}
