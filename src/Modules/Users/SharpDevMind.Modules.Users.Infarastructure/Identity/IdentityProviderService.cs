using System.Net;
using Microsoft.Extensions.Logging;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Users.Application.Abstractions.Identity;

namespace SharpDevMind.Modules.Users.Infrastructure.Identity;

internal sealed class IdentityProviderService(KeyCloakClient keyCloakClient, ILogger<IdentityProviderService> logger)
    : IIdentityProviderService
{
    private const string PasswordCredentialType = "password";

    // POST /admin/realms/{realm}/users
    public async Task<Result<string>> RegisterUserAsync(UserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            [new CredentialRepresentation(PasswordCredentialType, user.Password, false)]);

        try
        {
            string identityId = await keyCloakClient.RegisterUserAsync(userRepresentation, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User registration failed");

            return Result.Failure<string>(IdentityProviderErrors.EmailIsNotUnique);
        }
    }
    public async Task<Result<string>> UpdateUserAsync(UpdateUserModel user, CancellationToken cancellationToken = default)
    {
        var userRepresentation = new UserRepresentation(
            user.Email,
            user.Email,
            user.FirstName,
            user.LastName,
            true,
            true,
            []);

        var passwordRepresentation = new PasswordRepresentation(
            "password",
            user.Password
            );


        try
        {
            string identityId = await keyCloakClient.UpdateUserAsync(userRepresentation, user.IdentityId, cancellationToken);
            await keyCloakClient.UpdateUserPasswordAsync(passwordRepresentation, user.IdentityId, cancellationToken);

            return identityId;
        }
        catch (HttpRequestException exception) when (exception.StatusCode == HttpStatusCode.Conflict)
        {
            logger.LogError(exception, "User update failed");

            return Result.Failure<string>(IdentityProviderErrors.UserUpdateFailed(user.IdentityId));
        }

    }
}
