using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Posts.PublicApi;
using SharpDevMind.Modules.Users.Application.Abstractions.Data;
using SharpDevMind.Modules.Users.Domain.Users;

namespace SharpDevMind.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IPostsApi postsApi)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var user = User.Create(request.Email, request.FirstName, request.LastName);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        await postsApi.CreateAuthorAsync(user.Id, user.Email, user.FirstName, user.LastName, cancellationToken);

        return user.Id;
    }
}
