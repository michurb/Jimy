namespace Jimy.Core.Exceptions;

public sealed class SetNotFoundException : CoreException
{
    public SetNotFoundException(int setNumber) 
        : base($"Set number {setNumber} was not found for this exercise.")
    {
    }
}