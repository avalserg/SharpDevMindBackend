using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Domain.Options;
public sealed class Option : Entity
{
    public Guid Id { get; private set; }
    public string Text { get; private set; }
    public Guid QuestionId { get; private set; }

    private Option() { }

    public static Option Create(string text, Guid questionId)
    {

        return new Option
        {
            Id = Guid.NewGuid(),
            Text = text,
            QuestionId = questionId
        };
    }
}
