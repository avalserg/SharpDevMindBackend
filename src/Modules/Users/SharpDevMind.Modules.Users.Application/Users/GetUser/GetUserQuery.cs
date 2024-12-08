using Evently.Common.Application.Messaging;

namespace SharpDevMind.Modules.Users.Application.Users.GetUser;

public sealed record GetUserQuery(Guid UserId) : IQuery<UserResponse>;
