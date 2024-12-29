using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.QuizResults;
using SharpDevMind.Modules.Quizzes.Domain.Users;

namespace SharpDevMind.Modules.Quizzes.Application.QuizResults.CreateQuizResult;

internal sealed class CreateQuizResultCommandHandler(
    IQuizResultRepository quizResultRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateQuizResultCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateQuizResultCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user == null)
        {
            return Result.Failure<Guid>(UserErrors.NotFound(request.UserId));
        }

        Result<QuizResult> result = QuizResult.Create(
            request.UserId,
            request.Score
           );

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        quizResultRepository.Insert(result.Value);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return result.Value.Id;
    }
}
