using Microsoft.EntityFrameworkCore;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories.RoleRepository;


public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _dbContext;

    public RoleRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Role> AddAsync(Role role)
    {
        var addedRole = await _dbContext.Roles.AddAsync(role);
        return addedRole.Entity;
    }

    public async Task<Role?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Role>> GetAllAsync()
    {
        return await _dbContext.Roles.ToListAsync();
    }

    public Task UpdateAsync(Role role)
    {
        _dbContext.Update(role);
        return Task.CompletedTask;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var internship = await _dbContext.InternshipForms.FirstOrDefaultAsync(x => x.Id == id);
        if (internship is not null)
        {
            _dbContext.Remove(internship);
        }
    }
}
