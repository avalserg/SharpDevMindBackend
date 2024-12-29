using SharpDevMind.Modules.Quizzes.Application.Options.GetOption;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.GetQuestion;

public sealed record QuestionResponse
{
    public Guid Id { get; init; }
    public string TextCondition { get; init; }

    public List<OptionResponse> Options { get; init; } = [];

};

