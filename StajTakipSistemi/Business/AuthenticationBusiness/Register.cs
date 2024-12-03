using MediatR;
using Microsoft.EntityFrameworkCore;
using StajTakipSistemi.Authentication.PasswordHasher;
using StajTakipSistemi.Authentication.TokenGenerator;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;
using StajTakipSistemi.Repositories.HistoryRepository;
using StajTakipSistemi.Repositories.RoleRepository;
using StajTakipSistemi.Repositories.UserRepository;
using StajTakipSistemi.Repositories.UserRoleRepository;

namespace StajTakipSistemi.Business.AuthenticationBusiness;

public record RegisterCommand(
    string FirstName,
    string LastName,
    string Email,
    string Password) : IRequest<AuthenticationResult>;


public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthenticationResult>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IHistoryRepository _historyRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly ApplicationDbContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork, 
        IHistoryRepository historyRepository, 
        IRoleRepository roleRepository, 
        IUserRoleRepository userRoleRepository, 
        ApplicationDbContext dbContext)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _historyRepository = historyRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _dbContext = dbContext;
    }    
    
    public async Task<AuthenticationResult> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistsByEmailAsync(command.Email))
        {
            throw new Exception("User already exist.");
        }

        var hashPasswordResult = _passwordHasher.HashPassword(command.Password);
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = command.FirstName,
            LastName = command.LastName,
            Email = command.Email,
            Password = hashPasswordResult,
        };

        var studentRole = await _dbContext.Roles.FirstOrDefaultAsync(x => x.Title == "student");
        await _userRepository.AddUserAsync(user);
        await _userRoleRepository.AddAsync(new UserRole()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            RoleId = studentRole!.Id
        });
        await _historyRepository.AddAsync(new History()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id
        });
        
        // Always should be in commands
        await _unitOfWork.CommitChangesAsync();

        var token = _jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(user, token);
    }
}