using Jimy.Core.ValueObjects;

namespace Jimy.Core.Exceptions;

public sealed class InvalidWeightException : CoreException
{
    public InvalidWeightException() : base("Weight is invalid.") { }
}