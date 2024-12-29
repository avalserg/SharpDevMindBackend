using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.Users;

namespace SharpDevMind.Modules.Quizzes.Application.Users.UpdateUSer;

internal sealed class UpdateUserCommandHandler(IUserRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? author = await authorRepository.GetAsync(request.UserId, cancellationToken);

        if (author is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        author.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
