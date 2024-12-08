using MediatR;
using SharpDevMind.Modules.Users.Domain.Abstractions;

namespace Evently.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
