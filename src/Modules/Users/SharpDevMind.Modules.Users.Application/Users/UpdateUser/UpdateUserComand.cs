﻿using SharpDevMind.Common.Application.Messaging;

namespace SharpDevMind.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName) : ICommand;
