using SharpDevMind.Common.Application.Clock;

namespace SharpDevMind.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
