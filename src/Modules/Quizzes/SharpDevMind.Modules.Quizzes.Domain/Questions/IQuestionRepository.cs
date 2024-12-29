namespace SharpDevMind.Modules.Quizzes.Domain.Questions;
public interface IQuestionRepository
{
    Task<Question?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    void Insert(Question question);
}
