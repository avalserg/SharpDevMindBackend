using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Domain.Options;

namespace SharpDevMind.Modules.Quizzes.Domain.Questions;
public sealed class Question : Entity
{
    private List<Option> _options = [];
    private Question()
    {
    }

    public Guid Id { get; private set; }
    public string TextCondition { get; private set; }

    public IReadOnlyCollection<Option> ListOptions => _options.ToList();
    public Guid CorrectOptionId { get; set; }
    public static Question Create(Guid id, string textCondition, List<Option> options, Guid correctOptionId)
    {
        return new Question
        {
            Id = id,
            TextCondition = textCondition,
            _options = options,
            CorrectOptionId = correctOptionId
        };
    }

    public void Update(string textCondition)
    {
        TextCondition = textCondition;

    }
}
