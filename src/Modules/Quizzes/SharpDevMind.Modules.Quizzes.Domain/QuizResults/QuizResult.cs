using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Domain.QuizResults;
public sealed class QuizResult : Entity
{
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }
    public int Score { get; private set; }
    public DateTime CompletedAt { get; private set; }

    private QuizResult() { }

    public static QuizResult Create(Guid userId, int score)
    {
        return new QuizResult
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Score = score,
            CompletedAt = DateTime.Now.ToUniversalTime(),
        };
    }
}
