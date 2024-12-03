
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories.RoleRepository;

public interface IRoleRepository
{
    Task<Role> AddAsync(Role role);
    Task<Role?> GetByIdAsync(Guid id);
    Task<List<Role>> GetAllAsync();
    Task UpdateAsync(Role history);
    Task DeleteByIdAsync(Guid id);
}