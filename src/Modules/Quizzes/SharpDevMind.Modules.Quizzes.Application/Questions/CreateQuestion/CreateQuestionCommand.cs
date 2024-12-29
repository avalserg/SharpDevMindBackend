using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.CreateQuestion;

public sealed record CreateQuestionCommand(
    string TextCondition, List<string> Options, int IndexOfTheRightAnswer) : ICommand<Guid>;
