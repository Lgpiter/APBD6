using WebApplication1.Models;

namespace WebApplication1.Requests;


public class PrescriptionRequest
{
    public PatientDto Patient { get; set; }
    public int IdDoctor { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentDto> Medicaments { get; set; }
}