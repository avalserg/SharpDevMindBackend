namespace SharpDevMind.Modules.Quizzes.Application.Options.GetOption;

public sealed record OptionResponse
{
    public Guid Id { get; init; }
    public Guid QuestionId { get; init; }
    public string Text { get; init; }
}

