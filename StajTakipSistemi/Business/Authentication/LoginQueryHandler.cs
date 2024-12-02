using MediatR;
using StajTakipSistemi.Authentication;
using StajTakipSistemi.Authentication.PasswordHasher;
using StajTakipSistemi.Authentication.TokenGenerator;
using StajTakipSistemi.Repositories;

namespace StajTakipSistemi.Business.Authentication;

[Authorize(Roles = "admin")]
public record LoginQuery(
    string Email,
    string Password) : IRequest<AuthenticationResult>;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }
    
    public async Task<AuthenticationResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(query.Email);
        // var user = await _userRepository.GetByEmailWithRolesAsync(query.Email);

        return user is null || !user.IsCorrectPasswordHash(query.Password, _passwordHasher)
            ? throw new Exception("Invalid credentials")
            : new AuthenticationResult(user, _jwtTokenGenerator.GenerateToken(user));
    }
}