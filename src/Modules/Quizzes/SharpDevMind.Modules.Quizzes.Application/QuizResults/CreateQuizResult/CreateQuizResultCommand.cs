using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.CreateQuizResult;

public sealed record CreateQuizResultCommand(
    Guid UserId, int Score) : ICommand<Guid>;
