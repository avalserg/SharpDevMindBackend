using MediatR;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
