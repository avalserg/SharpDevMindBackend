using MediatR;
using SharpDevMind.Common.Domain;

namespace SharpDevMind.Common.Application.Messaging;
public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
