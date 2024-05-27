namespace WebApplication1.Models;

public class Doctor
{
    public int IdDoctor { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public ICollection<Prescription> Presriptions { get; set; }
}