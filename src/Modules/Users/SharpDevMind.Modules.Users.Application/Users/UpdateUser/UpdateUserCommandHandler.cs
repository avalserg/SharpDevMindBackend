using SharpDevMind.Common.Application.Messaging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Users.Application.Abstractions.Data;
using SharpDevMind.Modules.Users.Application.Abstractions.Identity;
using SharpDevMind.Modules.Users.Domain.Users;

namespace SharpDevMind.Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IIdentityProviderService identityProviderService, IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        Result<string> result = await identityProviderService.UpdateUserAsync(
            new UpdateUserModel(user.Email, request.Password, request.FirstName, request.LastName, user.IdentityId),
            cancellationToken);

        if (result.IsFailure)
        {
            return Result.Failure<Guid>(result.Error);
        }

        user.Update(request.FirstName, request.LastName);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
