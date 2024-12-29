using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Quizzes.Application.Abstractions.Data;
using SharpDevMind.Modules.Quizzes.Domain.Users;

namespace SharpDevMind.Modules.Quizzes.Application.Users.CreateUser;

internal sealed class CreatePostCommandHandler(IUserRepository authorRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var author = User.Create(request.UserId, request.Email, request.FirstName, request.LastName);

        authorRepository.Insert(author);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
