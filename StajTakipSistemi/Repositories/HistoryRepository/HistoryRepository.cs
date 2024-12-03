using StajTakipSistemi.Models;

namespace StajTakipSistemi.Repositories.HistoryRepository;

public interface IHistoryRepository
{
    Task<History> AddAsync(History history);
    Task<History?> GetByIdAsync(Guid id);
    Task<List<History>> GetAllAsync();
    Task UpdateAsync(History history);
    Task DeleteByIdAsync(Guid id);
}