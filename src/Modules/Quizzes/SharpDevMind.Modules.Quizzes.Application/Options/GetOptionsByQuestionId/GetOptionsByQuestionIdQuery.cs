using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Options.GetOptionsByQuestionId;

public sealed record GetOptionsByQuestionIdQuery(Guid QuestionId) : IQuery<IReadOnlyCollection<OptionByQuestionIdResponse>>;
