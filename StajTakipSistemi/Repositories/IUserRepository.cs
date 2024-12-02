using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories;

public interface IUserRepository
{
    Task AddUserAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByEmailAsync(string email);
    Task<User?> GetByIdAsync(Guid userId);
    Task UpdateAsync(User user);
    
    Task<User?> GetByEmailWithRolesAsync(string email);
}