using Microsoft.EntityFrameworkCore;
using StajTakipSistemi.Database;

namespace StajTakipSistemi.Repositories.HistoryRepository;

public class HistoryRepository : IHistoryRepository
{
    private readonly ApplicationDbContext _dbContext;

    public HistoryRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Models.History> AddAsync(Models.History history)
    {
        var addedInternshipForm = await _dbContext.Histories.AddAsync(history);
        return addedInternshipForm.Entity;
    }

    public async Task<Models.History?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Histories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<Models.History>> GetAllAsync()
    {
        return await _dbContext.Histories.ToListAsync();
    }

    public Task UpdateAsync(Models.History history)
    {
        _dbContext.Update(history);
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