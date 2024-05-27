using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Service;

public interface IPrescriptionService
{
    Task<Prescription> AddPrescriptionAsync(PrescriptionRequest request);
}