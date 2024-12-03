namespace StajTakipSistemi.Business.AuthenticationBusiness;

public record AuthenticationResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    string Token);