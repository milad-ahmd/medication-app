using MyMedicationApp.Domain.Entities;

namespace MyMedicationApp.Application.Interfaces;

public interface IMedicationRepository
{
    Task<List<Medication>> GetAllAsync();
    Task<Medication?> GetByIdAsync(Guid id);
    Task<Medication> CreateAsync(Medication medication);
    Task<bool> DeleteAsync(Guid id);
}