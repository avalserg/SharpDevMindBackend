﻿using Evently.Common.Application.Messaging;
using SharpDevMind.Modules.Users.Application.Abstractions.Data;
using SharpDevMind.Modules.Users.Domain.Abstractions;
using SharpDevMind.Modules.Users.Domain.Users;

namespace SharpDevMind.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {

        var user = User.Create(request.Email, request.FirstName, request.LastName);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}