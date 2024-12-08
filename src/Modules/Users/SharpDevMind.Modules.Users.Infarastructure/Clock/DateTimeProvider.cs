using SharpDevMind.Modules.Users.Application.Abstractions.Clock;

namespace Evently.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
