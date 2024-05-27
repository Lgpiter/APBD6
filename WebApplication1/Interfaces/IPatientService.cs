using WebApplication1.Models;

namespace WebApplication1.Service;

public interface IPatientService
{
    Task<Patient> GetPatientDetailsAsync(int id);
}