using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Users.Application.Abstractions.Identity;

public static class IdentityProviderErrors
{
    public static readonly Error EmailIsNotUnique = Error.Conflict(
        "Identity.EmailIsNotUnique",
        "The specified email is not unique.");

    public static Error UserUpdateFailed(string identityId) => Error.Conflict(
        "Identity.UserUpdateFailed",
        $"The user with id {identityId} failed update.");
}
