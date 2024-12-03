using Microsoft.EntityFrameworkCore;
using StajTakipSistemi.Database;
using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories.InternshipFormRepository;

public class InternshipFormRepository : IInternshipFormRepository
{
    private readonly ApplicationDbContext _dbContext;

    public InternshipFormRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<InternshipForm> AddAsync(InternshipForm internshipForm)
    {
        var addedInternshipForm = await _dbContext.InternshipForms.AddAsync(internshipForm);
        return addedInternshipForm.Entity;
    }

    public async Task<InternshipForm?> GetByIdAsync(Guid id)
    {
        return await _dbContext.InternshipForms.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<InternshipForm>> GetAllAsync()
    {
        return await _dbContext.InternshipForms.ToListAsync();
    }

    public Task UpdateAsync(InternshipForm internshipForm)
    {
        _dbContext.Update(internshipForm);
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