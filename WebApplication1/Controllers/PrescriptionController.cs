using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;
using WebApplication1.Requests;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/prescriptions")]
public class PrescriptionController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpPost]
    public async Task<IActionResult> AddDescription([FromBody] PrescriptionRequest request)
    {
        try
        {
            var prescription = await _prescriptionService.AddPrescriptionAsync(request);
            return Ok(prescription);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

}