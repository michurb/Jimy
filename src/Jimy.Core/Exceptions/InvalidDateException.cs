namespace Jimy.Core.Exceptions;

public sealed class InvalidDateException : CoreException
{
    public InvalidDateException(string message) : base(message) { }
}