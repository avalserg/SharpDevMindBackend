namespace SharpDevMind.Modules.Users.Application.Abstractions.Identity;

public sealed record UpdateUserModel(string Email, string Password, string FirstName, string LastName, string IdentityId);
