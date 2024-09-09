namespace Jimy.Core.Exceptions;

public sealed class InvalidRepsException : CoreException
{
    public int Value { get; }

    public InvalidRepsException(int value) : base($"Invalid number of reps: {value}")
    {
        Value = value;
    }
}