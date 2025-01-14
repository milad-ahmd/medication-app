using MyMedicationApp.Application.Interfaces;
using MyMedicationApp.Domain.Entities;

namespace MyMedicationApp.Application.Services;

public class MedicationService
{
    private readonly IMedicationRepository _medicationRepository;

    public MedicationService(IMedicationRepository medicationRepository)
    {
        _medicationRepository = medicationRepository;
    }

    public async Task<List<Medication>> GetAllMedicationsAsync()
    {
        return await _medicationRepository.GetAllAsync();
    }

    public async Task<Medication> CreateMedicationAsync(string name, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentException("Quantity must be greater than zero.");

        var medication = new Medication
        {
            Id = Guid.NewGuid(),
            Name = name,
            Quantity = quantity,
            CreationDate = DateTime.UtcNow
        };

        return await _medicationRepository.CreateAsync(medication);
    }

    public async Task<bool> DeleteMedicationAsync(Guid id)
    {
        return await _medicationRepository.DeleteAsync(id);
    }
}