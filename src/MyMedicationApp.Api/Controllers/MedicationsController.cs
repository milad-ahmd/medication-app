using Microsoft.AspNetCore.Mvc;
using MyMedicationApp.Application.Services;
using MyMedicationApp.Domain.Entities;

namespace MyMedicationApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MedicationsController : ControllerBase
{
    private readonly MedicationService _medicationService;

    public MedicationsController(MedicationService medicationService)
    {
        _medicationService = medicationService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Medication>>> GetAll()
    {
        var medications = await _medicationService.GetAllMedicationsAsync();
        return Ok(medications);
    }

    [HttpPost]
    public async Task<ActionResult<Medication>> Create([FromBody] MedicationCreateRequest request)
    {
        try
        {
            var medication = await _medicationService.CreateMedicationAsync(request.Name, request.Quantity);
            return CreatedAtAction(nameof(GetAll), new { id = medication.Id }, medication);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var success = await _medicationService.DeleteMedicationAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}

public class MedicationCreateRequest
{
    public string Name { get; set; } = null!;
    public int Quantity { get; set; }
}