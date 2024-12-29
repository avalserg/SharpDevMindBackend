using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.Options;
using SharpDevMind.Modules.Quizzes.Domain.Questions;

namespace SharpDevMind.Modules.Quizzes.Application.Questions.CreateQuestion;

internal sealed class CreateQuestionCommandHandler(
    IQuestionRepository questionRepository,
    IOptionRepository optionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateQuestionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();

        var options = new List<Option>();

        foreach (string option in request.Options)
        {
            options.Add(Option.Create(option, guid));
        }

        if (request.IndexOfTheRightAnswer > options.Capacity - 1)
        {
            throw new Exception("Index more than Count options");
        }

        Guid getCorrectOption = options[request.IndexOfTheRightAnswer].Id;

        Result<Question> result = Question.Create(
            guid,
            request.TextCondition,
            options,
            getCorrectOption
           );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        questionRepository.Insert(result.Value);
        optionRepository.InsertMany(options);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
