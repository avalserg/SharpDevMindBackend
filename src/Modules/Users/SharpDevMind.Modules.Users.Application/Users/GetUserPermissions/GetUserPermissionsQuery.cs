using SharpDevMind.Common.Application.Authorization;
using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
