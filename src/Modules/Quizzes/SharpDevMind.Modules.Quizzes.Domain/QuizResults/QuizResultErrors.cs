using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Domain.QuizResults;
public static class QuizResultErrors
{
    public static Error NotFound(Guid quizId) =>
        Error.NotFound("Quiz.NotFound", $"The quiz with the identifier {quizId} was not found");
}
