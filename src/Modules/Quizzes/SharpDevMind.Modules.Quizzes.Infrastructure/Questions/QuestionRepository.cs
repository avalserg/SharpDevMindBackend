using Microsoft.EntityFrameworkCore;
using SharpDevMind.Modules.Quizzes.Domain.Questions;
using SharpDevMind.Modules.Quizzes.Infrastructure.Database;

namespace SharpDevMind.Modules.Quizzes.Infrastructure.Questions;
internal sealed class QuestionRepository(QuizzesDbContext context) : IQuestionRepository
{
    public async Task<Question?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Questions.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);

    }

    public void Insert(Question question)
    {
        context.Questions.Add(question);
    }
}
