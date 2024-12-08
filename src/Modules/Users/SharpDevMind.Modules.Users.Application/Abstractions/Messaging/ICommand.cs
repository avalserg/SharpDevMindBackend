using MediatR;
using SharpDevMind.Modules.Users.Domain.Abstractions;

namespace Evently.Common.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
