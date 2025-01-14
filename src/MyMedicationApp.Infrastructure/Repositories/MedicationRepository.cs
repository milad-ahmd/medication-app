using MyMedicationApp.Application.Interfaces;
using MyMedicationApp.Domain.Entities;
using MyMedicationApp.Infrastructure.Data;

namespace MyMedicationApp.Infrastructure.Repositories;

public class MedicationRepository : IMedicationRepository
{
    private readonly AppDbContext _dbContext;

    public MedicationRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Medication>> GetAllAsync()
    {
        return await _dbContext.Medications.ToListAsync();
    }

    public async Task<Medication?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Medications.FindAsync(id);
    }

    public async Task<Medication> CreateAsync(Medication medication)
    {
        _dbContext.Medications.Add(medication);
        await _dbContext.SaveChangesAsync();
        return medication;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existing = await _dbContext.Medications.FindAsync(id);
        if (existing == null) return false;

        _dbContext.Medications.Remove(existing);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}