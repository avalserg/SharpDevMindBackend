using MediatR;
using SharpDevMind.Common.Application.Authorization;
using SharpDevMind.Common.Domain;
using SharpDevMind.Modules.Users.Application.Users.GetUserPermissions;

namespace SharpDevMind.Modules.Users.Infrastructure.Authorization;

internal sealed class PermissionService(ISender sender) : IPermissionService
{
    public async Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId)
    {
        return await sender.Send(new GetUserPermissionsQuery(identityId));
    }
}
