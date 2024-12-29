using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Options.GetOption;

public sealed record GetOptionQuery(Guid OptionId) : IQuery<OptionResponse>;
