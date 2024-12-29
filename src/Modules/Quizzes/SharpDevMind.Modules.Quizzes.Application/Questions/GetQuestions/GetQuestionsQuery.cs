using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestions;

public sealed record GetQuestionsQuery : IQuery<IReadOnlyCollection<QuestionResponse>>;
