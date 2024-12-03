namespace StajTakipSistemi.Repositories.UserRepository;

public interface IUserRepository
{
    Task AddUserAsync(Models.User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<Models.User?> GetByEmailAsync(string email);
    Task<Models.User?> GetByIdAsync(Guid userId);
    Task UpdateAsync(Models.User user);
    
    Task<Models.User?> GetByEmailWithRolesAsync(string email);
}