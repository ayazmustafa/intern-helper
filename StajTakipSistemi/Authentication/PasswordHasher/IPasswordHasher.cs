namespace StajTakipSistemi.Authentication.PasswordHasher;

public interface IPasswordHasher
{
    public string HashPassword(string password);
    bool IsCorrectPassword(string password, string hash);
}