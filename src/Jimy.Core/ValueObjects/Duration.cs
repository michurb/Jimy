using Jimy.Core.Exceptions;

namespace Jimy.Core.ValueObjects;

public sealed record Duration
{
    public int Minutes { get; }

    public Duration(int minutes)
    {
        if (minutes <= 0)
        {
            throw new InvalidDurationException(minutes);
        }
        Minutes = minutes;
    }
}