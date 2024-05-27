using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Service;

public class PatientService : IPatientService
{
   private readonly ApplicationContext _applicationContext;

   public PatientService(ApplicationContext context)
   {
      _applicationContext = context;
   }

   public async Task<Patient> GetPatientDetailsAsync(int id)
   {
      var patient = await _applicationContext.Patients
         .Include(p => p.Prescriptions)
         .ThenInclude(pr => pr.PrescriptionMedicaments)
         .ThenInclude(pm => pm.Medicament)
         .Include(p => p.Prescriptions)
         .ThenInclude(pr => pr.Doctor)
         .FirstOrDefaultAsync(p => p.IdPatient == id);

      if (patient == null)
         throw new KeyNotFoundException("Patient not Found");

      return patient;

   }
}