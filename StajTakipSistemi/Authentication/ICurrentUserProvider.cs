namespace StajTakipSistemi.Authentication;

public interface ICurrentUserProvider
{
    CurrentUser GetCurrentUser();
}