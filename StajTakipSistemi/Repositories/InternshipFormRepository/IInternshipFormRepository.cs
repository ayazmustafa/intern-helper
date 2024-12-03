namespace StajTakipSistemi.Repositories.InternshipFormRepository;

public interface IInternshipFormRepository
{
    Task<Models.InternshipForm> AddAsync(Models.InternshipForm internshipForm);
    Task<Models.InternshipForm?> GetByIdAsync(Guid id);
    Task<List<Models.InternshipForm>> GetAllAsync();
    Task UpdateAsync(Models.InternshipForm internshipForm);
    Task DeleteByIdAsync(Guid id);
}