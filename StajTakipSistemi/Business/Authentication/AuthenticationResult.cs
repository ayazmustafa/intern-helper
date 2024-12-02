using StajTakipSistemi.Models;

namespace StajTakipSistemi.Business.Authentication;

public record AuthenticationResult(
    User User,
    string Token);