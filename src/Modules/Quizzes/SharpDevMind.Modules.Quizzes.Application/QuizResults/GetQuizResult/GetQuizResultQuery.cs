using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizResult;

public sealed record GetQuizResultQuery(Guid Id) : IQuery<QuizResultResponse>;
