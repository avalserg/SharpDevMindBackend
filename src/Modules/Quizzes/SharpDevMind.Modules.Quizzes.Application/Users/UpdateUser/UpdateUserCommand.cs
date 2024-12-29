using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Quizzes.Application.Users.UpdateUSer;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
