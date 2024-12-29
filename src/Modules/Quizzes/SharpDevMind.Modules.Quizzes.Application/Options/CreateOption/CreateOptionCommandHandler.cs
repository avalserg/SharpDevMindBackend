using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.Options;
using SharpDevMind.Modules.Quizzes.Domain.Questions;

namespace SharpDevMind.Modules.Quizzes.Application.Options.CreateOption;

internal sealed class CreateOptionCommandHandler(
    IOptionRepository optionRepository,
    IQuestionRepository questionRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateOptionCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
    {
        Question? question = await questionRepository.GetAsync(request.QuestionId, cancellationToken);

        if (question is null)
        {
            return Result.Failure<Guid>(QuestionErrors.NotFound(request.QuestionId));
        }

        Result<Option> result = Option.Create(
            request.Text,
            question.Id
           );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        optionRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
