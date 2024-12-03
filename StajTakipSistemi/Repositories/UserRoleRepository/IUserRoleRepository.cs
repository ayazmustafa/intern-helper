
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories.UserRoleRepository;

public interface IUserRoleRepository
{
    Task<UserRole> AddAsync(UserRole userRole);
    Task<UserRole?> GetByIdAsync(Guid id);
    Task<List<UserRole>> GetAllAsync();
    Task UpdateAsync(UserRole userRole);
    Task DeleteByIdAsync(Guid id);
}