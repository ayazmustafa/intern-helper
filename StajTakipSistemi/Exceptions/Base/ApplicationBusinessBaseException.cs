namespace StajTakipSistemi.Exceptions.Base;

public abstract class ApplicationBusinessException : ApplicationBaseException
{
    protected ApplicationBusinessException(){ }
    protected ApplicationBusinessException(string? message) : base(message){}
    protected ApplicationBusinessException(string? message, Exception? innerException) : base(message, innerException) {}
}
