using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Domain.Questions;
public static class QuestionErrors
{
    public static Error NotFound(Guid questionId) =>
        Error.NotFound("Question.NotFound", $"The question with the identifier {questionId} was not found");
}
