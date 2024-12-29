using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.GetQuizzesResultsByUserId;

public sealed record GetQuizzesResultsByUserIdQuery(Guid UserId) : IQuery<IReadOnlyCollection<QuizzesResultsByUserIdResponse>>;
