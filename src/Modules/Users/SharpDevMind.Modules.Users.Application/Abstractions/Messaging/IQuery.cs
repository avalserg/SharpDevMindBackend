using MediatR;
using SharpDevMind.Modules.Users.Domain.Abstractions;

namespace Evently.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
