using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Users.Application.Abstractions.Identity;

public interface IIdentityProviderService
{
    Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default);
    Task<Result<string>> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default);
}
