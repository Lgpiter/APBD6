using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Service;

using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Requests;



public class PrescriptionService : IPrescriptionService
{
    private readonly ApplicationContext _context;

    public PrescriptionService(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Prescription> AddPrescriptionAsync(PrescriptionRequest request)
    {
        if (request == null || request.Medicaments == null || request.Patient == null)
            throw new ArgumentException("Invalid request");
        
        var patient = await _context.Patients.FindAsync(request.Patient.IdPatient)
            ?? new Patient
            {
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.Birthdate
            };

        var doctor = await _context.Doctors.FindAsync(request.IdDoctor);
        
        if (doctor == null)
            throw new KeyNotFoundException("Doctor not found");

        if (request.DueDate < request.Date)
        {
            throw new ArgumentException("DueDate must be greater than Date.");
        }
        
        if (request.Medicaments.Count > 10)
            throw new ArgumentException("A prescription can include a maximum of 10 medicaments");

        foreach (var med in request.Medicaments)
        {
            if (!await _context.Medicaments.AnyAsync(m => m.IdMedicament == med.IdMedicament))
                throw new KeyNotFoundException("Medicament not found");
        }
        
        var prescription = new Prescription
        {
            Patient = patient,
            Doctor = doctor,
            Date = request.Date,
            DueDate = request.DueDate,
            PrescriptionMedicaments = request.Medicaments.Select(m => new Prescription_Medicament()
            {
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Details
            }).ToList()
        };

        _context.Prescriptions.Add(prescription);
        await _context.SaveChangesAsync();

        return prescription;
    }
    
}