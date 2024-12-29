namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizzesResultsByUserId;

public sealed record QuizzesResultsByUserIdResponse(
    Guid Id,
    Guid UserId,
    int Score,
    DateTime CompletedAt
);
