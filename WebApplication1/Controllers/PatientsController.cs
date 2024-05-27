using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/patients")]
public class PatientsController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientsController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPatientDetails(int id)
    {
        try
        {
            var patient = await _patientService.GetPatientDetailsAsync(id);

            var result = new
            {
                patient.IdPatient,
                patient.FirstName,
                patient.LastName,
                patient.BirthDate,
                Prescriptions = patient.Prescriptions.OrderBy(pr => pr.DueDate).Select(pr => new
                {
                    pr.IdPrescription,
                    pr.Date,
                    pr.DueDate,
                    Medicaments = pr.PrescriptionMedicaments.Select(pm => new
                    {
                        pm.Medicament.IdMedicament,
                        pm.Medicament.Name,
                        pm.Dose,
                        pm.Details,
                        pm.Medicament.Description,
                        pm.Medicament.Type
                    }),
                    Doctor = new
                    {
                        pr.Doctor.IdDoctor,
                        pr.Doctor.FirstName,
                        pr.Doctor.LastName,
                        pr.Doctor.Email
                    }
                })
            };

            return Ok(result);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}