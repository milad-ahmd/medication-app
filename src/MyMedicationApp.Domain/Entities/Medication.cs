namespace MyMedicationApp.Domain.Entities;

public class Medication
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
    public DateTime CreationDate { get; set; }
}