using MediatR;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
