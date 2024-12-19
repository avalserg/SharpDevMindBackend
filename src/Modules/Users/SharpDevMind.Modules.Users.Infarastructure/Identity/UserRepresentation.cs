namespace SharpDevMind.Modules.Users.Infrastructure.Identity;

internal sealed record UserRepresentation(
    string Username,
    string Email,
    string FirstName,
    string LastName,
    bool EmailVerified,
    bool Enabled,
    CredentialRepresentation[] Credentials);

internal sealed record PasswordRepresentation(string Type, string Value, bool Temporary = false);

