using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Users.CreateUser;

public sealed record CreateUserCommand(Guid UserId, string Email, string FirstName, string LastName)
    : ICommand;
