namespace SharpDevMind.Modules.Quizzes.Domain.QuizResults;
public interface IQuizResultRepository
{
    Task<QuizResult?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(QuizResult quizResult);
}
