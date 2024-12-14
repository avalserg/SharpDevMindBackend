﻿using SharpDevMind.Common.Domain;

namespace SharpDevMind.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}