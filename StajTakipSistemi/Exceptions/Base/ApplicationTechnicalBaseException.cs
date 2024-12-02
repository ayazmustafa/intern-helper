namespace StajTakipSistemi.Exceptions.Base;

public abstract class ApplicationTechnicalException : ApplicationBaseException 
{
    protected ApplicationTechnicalException(){ }
    protected ApplicationTechnicalException(string? message) : base(message){}
    protected ApplicationTechnicalException(string? message, Exception? innerException) : base(message, innerException){}
}