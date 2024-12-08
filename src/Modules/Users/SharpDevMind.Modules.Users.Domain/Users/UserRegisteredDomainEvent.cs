using SharpDevMind.Modules.Users.Domain.Abstractions;

namespace SharpDevMind.Modules.Users.Domain.Users;

public sealed class UserRegisteredDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
