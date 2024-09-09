namespace Jimy.Core.Exceptions;

public sealed class InvalidSetsException : CoreException
{
    public int Value { get; }

    public InvalidSetsException(int value) : base($"Invalid number of sets: {value}")
    {
        Value = value;
    }
}