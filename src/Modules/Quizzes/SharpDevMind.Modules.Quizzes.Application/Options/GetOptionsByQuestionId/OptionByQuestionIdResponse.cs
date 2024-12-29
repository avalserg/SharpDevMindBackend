namespace SharpDevMind.Modules.Quizzes.Application.Options.GetOptionsByQuestionId;

public sealed record OptionByQuestionIdResponse(
    Guid Id,
    Guid QuestionId,
    string Text
);
