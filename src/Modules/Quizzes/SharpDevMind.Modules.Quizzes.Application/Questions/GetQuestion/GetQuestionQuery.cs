using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;

public sealed record GetQuestionQuery(Guid Id) : IQuery<QuestionResponse>;
