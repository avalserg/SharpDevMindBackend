namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizResult;

public sealed record QuizResultResponse
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public int Score { get; init; }
    public DateTime CompletedAt { get; init; }

};

