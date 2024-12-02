using StajTakipSistemi.Models;

namespace StajTakipSistemi.Authentication.TokenGenerator;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}