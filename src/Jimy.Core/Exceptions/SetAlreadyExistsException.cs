namespace Jimy.Core.Exceptions;

public sealed class SetAlreadyExistsException : CoreException
{
    public SetAlreadyExistsException(int setNumber) 
        : base($"Set number {setNumber} already exists for this exercise.")
    {
    }
}