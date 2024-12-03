using StajTakipSistemi.Models;

namespace StajTakipSistemi.Business.AuthenticationBusiness;

public record AuthenticationResult(
    User User,
    string Token);