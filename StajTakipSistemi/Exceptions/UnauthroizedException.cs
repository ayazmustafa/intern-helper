using StajTakipSistemi.Exceptions.Base;

namespace StajTakipSistemi.Exceptions;

public class UnauthroizedException : ApplicationBusinessException
{
    public UnauthroizedException()
    {
    }

    public UnauthroizedException(string? message) : base(message)
    {
    }

    public UnauthroizedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}