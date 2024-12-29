using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Options.CreateOption;

public sealed record CreateOptionCommand(
    Guid QuestionId,
    string Text) : ICommand<Guid>;
