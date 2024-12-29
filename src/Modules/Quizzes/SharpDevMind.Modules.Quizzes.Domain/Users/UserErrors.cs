using SharpDevMind.Common.Domain;

namespace SharpDevMind.Modules.Quizzes.Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) =>
        Error.NotFound("User.NotFound", $"The user with the identifier {userId} was not found");
}
