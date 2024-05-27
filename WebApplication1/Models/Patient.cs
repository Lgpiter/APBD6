

namespace WebApplication1.Models;

public class Patient
{
    public int IdPatient { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<Prescription> Prescriptions { get; set; }
}