using Jimy.Core.Abstraction;

namespace Jimy.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}