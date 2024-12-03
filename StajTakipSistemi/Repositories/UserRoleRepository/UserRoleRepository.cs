using Microsoft.EntityFrameworkCore;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories.UserRoleRepository;


public class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserRole> AddAsync(UserRole userRole)
    {
        var addedUserRole = await _dbContext.UserRoles.AddAsync(userRole);
        return addedUserRole.Entity;
    }

    public async Task<UserRole?> GetByIdAsync(Guid id)
    {
        return await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<UserRole>> GetAllAsync()
    {
        return await _dbContext.UserRoles.ToListAsync();
    }

    public Task UpdateAsync(UserRole userRole)
    {
        _dbContext.Update(userRole);
        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var internship = await _dbContext.UserRoles.FirstOrDefaultAsync(x => x.Id == id);
        if (internship is not null)
        {
            _dbContext.Remove(internship);
        }
    }
}
