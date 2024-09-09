namespace Jimy.Core.Exceptions;

public sealed class InvalidDurationException : CoreException
{
    public InvalidDurationException(int minutes) 
        : base($"Duration must be greater than 0 minutes. Provided: {minutes}")
    {
    }
}