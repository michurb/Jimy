namespace Jimy.Core.Exceptions;

public sealed class InvalidFullNameException : CoreException
{
    public InvalidFullNameException(string message) : base(message) { }
}